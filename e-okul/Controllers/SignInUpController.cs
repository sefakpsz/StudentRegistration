using e_okul.DataAccess.Repository.IRepository;
using e_okul.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http.Controllers;
using e_okul.DataAccess.Repository.IRepository;
using e_okul.Models;
using e_okul.Utility;
using e_okul.Models.ViewModels;

namespace e_okul.Controllers
{
    public class SignInUpController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public SignInUpController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public string CreateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            List<Claim> claims = new();

            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(ApplicationUser apUser)
        {
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Name == apUser.Name && x.Surname == apUser.Surname);

            if (user != null)
            {
                var jwt = CreateJwtToken(user);
                if (user.Role == SD.Role_User_Student)
                {
                    var stu = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == user.Name && x.Surname == user.Surname);
                    if (stu != null)
                    {
                        return RedirectToAction(nameof(StudentSignIn), new { name = apUser.Name, surname = apUser.Surname });
                    }
                    TempData["error"] = "User couldn't found";
                    return View();
                }
                else
                {
                    return RedirectToAction(nameof(Config), new { id = apUser.Id });
                }
            }
            else
            {
                TempData["error"] = "User couldn't found";
                return View();
            }
        }

        [HttpGet]
        public IActionResult StudentSignIn(string name, string surname)
        {
            var stu = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == name && x.Surname == surname);
            return View(stu);
        }

        [HttpPost]
        public IActionResult StudentSignIn(Student student)
        {
            var stu = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == student.Name && x.Surname == student.Surname);
            return RedirectToAction(nameof(Config), nameof(Student));
        }

        [HttpGet]
        public IActionResult Config(int id)
        {
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == id);

            UserVM vm = new();

            if (user != null)
            {
                if (user.Role == SD.Role_User_Student)
                {
                    vm.Student = new Student();
                    vm.Student = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == user.Name && x.Surname == user.Surname);
                    return View(vm);
                }
                return View();
            }
            else
            {
                return RedirectToAction(nameof(SignIn), new { apUser = user });
            }
        }

        [HttpPost]
        public IActionResult Config()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(ApplicationUser apUser)
        {
            var users = _unitOfWork.ApplicationUser.GetAll();
            foreach (var user in users)
            {
                if (apUser.Name == user.Name)
                {
                    if (apUser.Surname == user.Surname)
                    {
                        ModelState.AddModelError("name", "This user has already registered");
                    }
                }
            }

            if (ModelState.IsValid && ModelState.ErrorCount == 0)
            {
                if (apUser.Role == SD.Role_User_Student)
                {
                    return RedirectToAction(nameof(StudentSignUp), new { name = apUser.Name, surname = apUser.Surname });
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult StudentSignUp(string name, string surname)
        {
            Student stu = new();
            stu.Name = name;
            stu.Surname = surname;

            return View(stu);
        }

        [HttpPost]
        public IActionResult StudentSignUp(Student stu)
        {
            for (int i = 0; i < 10; i++)
            {
                if (stu.ClassLetter == i.ToString())
                {
                    ModelState.AddModelError("classletter", "You can enter only letters!");
                }
            }
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    Name = stu.Name,
                    Surname = stu.Surname,
                    Role = SD.Role_User_Student
                };
                _unitOfWork.ApplicationUser.Add(user);
                _unitOfWork.Student.Add(stu);
                _unitOfWork.Save();
                TempData["success"] = "Student Created Successfully";
                return RedirectToAction(nameof(SignIn));
            }
            TempData["error"] = "Student Couldn't Created!";
            return RedirectToAction(nameof(SignIn));
        }
    }
}