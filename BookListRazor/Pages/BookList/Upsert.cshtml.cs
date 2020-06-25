using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private readonly ApplicationDbContext Database;

        public UpsertModel(ApplicationDbContext pDataBase)
        {
            this.Database = pDataBase;
        }

        [BindProperty]
        public Book Book { get; set; }

        //id = id del libro que se requiere editar
        public async Task<IActionResult> OnGet(int? id)
        {
            this.Book = new Book();

            if (id == null)
            {
                //create
                return Page();
            }


            //update
            this.Book = await this.Database.Book.FirstOrDefaultAsync( u => u.Id == id );

            if (this.Book == null)
            {
                return NotFound();
            }

            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (this.Book.Id == 0)
                {
                    this.Database.Add(this.Book);
                }
                else
                {
                    this.Database.Update(this.Book);
                }

                await this.Database.SaveChangesAsync();

                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}