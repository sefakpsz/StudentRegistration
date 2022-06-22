using e_okul.DataAccess.Repository.IRepository;
using e_okul.Models;
using e_okul.Models.ViewModels;
using e_okul.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace e_okul.Controllers
{
    [Authorize(Roles = SD.Role_User_Student)]
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Config()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.Name).Value;

            var stu = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == claim);
            if (stu != null)
            {
                return View(stu);
            }
            else
            {
                var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Name == stu.Name && x.Surname == stu.Surname);
                return RedirectToAction(nameof(SignIn), nameof(SignInUpController), new { apUser = user });
            }
        }

        [HttpPost]
        public IActionResult Config(Student stu)
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
                stu.ClassLetter = stu.ClassLetter.ToUpper();
                _unitOfWork.Student.Update(stu);
                _unitOfWork.Save();

                TempData["success"] = "Student updated successfully!";
                return RedirectToAction(nameof(Config));
            }
            TempData["error"] = "Update couldn't be successful!";
            return RedirectToAction(nameof(Config));
        }

        [HttpGet]
        public IActionResult CourseSelection()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.Name).Value;

            var stu = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == claim);

            LectureListVM model = new LectureListVM();
            var lectures = _unitOfWork.Lecture.GetAll().ToList();
            var list = new List<LectureWithCheckVM>();

            foreach (var lecture in lectures)
            {
                var lectureWithCheckVM = new LectureWithCheckVM();
                lectureWithCheckVM.Lecture = lecture;
                list.Add(lectureWithCheckVM);
            }

            model.LecturesWithCheck = list;
            return View(model);
        }

        [HttpPost]
        public IActionResult CourseSelection(LectureListVM lectureList)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.Name).Value;

            var stu = _unitOfWork.Student.GetFirstOrDefault(x => x.Name == claim);

            var lectureNames = _unitOfWork.LectureName.GetAll();

            foreach (var lectures in lectureList.LecturesWithCheck)
            {
                if (lectures.Checked)
                {
                    foreach (var lectureName in lectureNames)
                    {
                        if (lectureName.LectureId == lectures.Lecture.Id)
                        {
                            TempData["error"] = lectures.Lecture.Name + " have already selected!";
                            ModelState.AddModelError("", "lsdjf");
                            return RedirectToAction(nameof(CourseSelection));
                        }
                    }
                    var currentLecture = _unitOfWork.Lecture.GetFirstOrDefault(x => x.Name == lectures.Lecture.Name);
                    if (currentLecture.Quota <= 0)
                    {
                        TempData["error"] = "There is any quota for " + lectures.Lecture.Name + ", please choose another lecture!";
                        return RedirectToAction(nameof(CourseSelection));
                    }
                    else
                    {
                        var lectureName = new LectureName()
                        {
                            StudenId = stu.Id,
                            LectureId = currentLecture.Id
                        };
                        _unitOfWork.LectureName.Add(lectureName);
                        --currentLecture.Quota;
                        _unitOfWork.Lecture.Update(currentLecture);
                    }
                }
            }

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}