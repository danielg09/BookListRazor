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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext DataBase;

        public IndexModel(ApplicationDbContext pDataBase)
        {
            this.DataBase = pDataBase;
        }

        public IEnumerable<Book> Books { get; set; }

        public async Task OnGet()
        {
            this.Books = await this.DataBase.Book.ToListAsync();

        }

        public async Task<IActionResult> OnPostDelete(int id) 
        {
            Book book = await this.DataBase.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            this.DataBase.Book.Remove(book);

            await this.DataBase.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}