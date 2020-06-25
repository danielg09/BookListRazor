using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext Database;

        public EditModel(ApplicationDbContext pDataBase)
        {
            this.Database = pDataBase;
        }

        [BindProperty]
        public Book Book { get; set; }

        //id = id del libro que se requiere editar
        public async Task OnGet(int id)
        {
            this.Book = await this.Database.Book.FindAsync(id);

        }

        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                Book BookFromDb = await this.Database.Book.FindAsync(this.Book.Id);

                BookFromDb.Name = this.Book.Name;
                BookFromDb.Author = this.Book.Author;
                BookFromDb.ISBN = this.Book.ISBN;

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