﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp3.Componens
{
    class Navigation
    {
        public static List<Nav> navs = new List<Nav>();
        public static MainWindow main;
        public static void NextPage(Nav nav)
        {
            navs.Add(nav);
            Update(nav);

        }
        public static void Update(Nav nav)
        {
            main.Mytitle.Text = nav.Title;
            main.MyFrame.Navigate(nav.Page);


        }
    }
    class Nav
    {
        public string Title { get; set; }
        public Page Page { get; set; }
        public Nav(string Title, Page Page)
        {
            this.Title = Title;
            this.Page = Page;
        }
    }
}
