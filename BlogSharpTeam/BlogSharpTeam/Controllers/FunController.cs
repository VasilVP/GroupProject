using BlogSharpTeam.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSharpTeam.Controllers
{
    public class FunController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Fun
        static int rowsCount = 3;
        static int colsCount = 9;
        static string[,] fruits = GenerateRandomFruits();
        static int score = 0;
        static int dynamiteCount = CountDynamites();
        static bool gameOver = false;
        static bool victory = false;

        static string[,] GenerateRandomFruits()
        {
            var rand = new Random();
            fruits = new string[rowsCount, colsCount];

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    var r = rand.Next(9);

                    if (r < 2)
                    {
                        fruits[row, col] = "Borisov-Istanbul";
                    }
                    else if (r == 3)
                    {
                        fruits[row, col] = "Mestan-lubovnik";
                    }
                    else if (r == 4)
                    {
                        fruits[row, col] = "peevski";
                    }
                    else if (r == 5)
                    {
                        fruits[row, col] = "prof-vuchkov";
                    }
                    else if (r == 6)
                    {
                        fruits[row, col] = "mestan";
                    }
                    else if (r == 6)
                    {
                        fruits[row, col] = "Zvetanov_kolaj1";
                    }
                    else if (r < 8)
                    {
                        fruits[row, col] = "mishka";
                    }
                    else
                    {
                        fruits[row, col] = "dynamite";
                    }
                }
            }

            return fruits;
        }

        static int CountDynamites()
        {
            int count = 0;

            for (int row = 0; row < rowsCount; row++)
            {
                for (int col = 0; col < colsCount; col++)
                {
                    if (fruits[row, col] == "dynamite")
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public ActionResult Reset()
        {
            fruits = GenerateRandomFruits();
            gameOver = false;
            victory = false;
            score = 0;
            dynamiteCount = CountDynamites();
            return RedirectToAction("FunIndex");
        }

        public ActionResult FireTop(int position)
        {
            return Fire(position, 0, 1);
        }

        public ActionResult FireBottom(int position)
        {
            return Fire(position, rowsCount - 1, -1);
        }

        private ActionResult Fire(int position, int startRow, int step)
        {
            var col = position * (colsCount - 1) / 100;
            var row = startRow;

            while (row >= 0 && row < rowsCount)
            {
                var fruit = fruits[row, col];

                if (fruit == "Borisov-Istanbul" || fruit == "Dogan" || fruit == "Mestan-lubovnik" || fruit == "mestan" || fruit == "mishka" || fruit == "peevski" || fruit == "prof-vuchkov" || fruit == "Zvetanov_kolaj1")
                {
                    score++;
                    fruits[row, col] = "empty1";

                    if (score + dynamiteCount == 27)
                    {
                        victory = true;
                        var userData = db.Users.Find(User.Identity.GetUserId());

                        if (userData != null)
                        {
                            userData.TotalScore += score;
                            db.SaveChanges();
                        }
                    }

                    break;
                }
                else if (fruit == "dynamite")
                {
                    gameOver = true;
                    break;
                }

                row = row + step;
            }

            return RedirectToAction("FunIndex");
        }

        public ActionResult FunIndex()
        {
            ViewBag.rowsCount = rowsCount;
            ViewBag.colsCount = colsCount;
            ViewBag.fruits = fruits;
            ViewBag.score = score;
            ViewBag.dynamiteCount = dynamiteCount;
            ViewBag.victory = victory;
            ViewBag.gameOver = gameOver;
            ViewBag.AllUsers = db.Users.OrderByDescending(u => u.TotalScore).ToList();

            return View();
        }
        public  ActionResult Index()
        {
            return View();
        }
    }
}