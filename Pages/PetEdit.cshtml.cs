using demowebapp.DAL;
using demowebapp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace demowebapp.Pages;

public class PetEditModel : PageModel
{
    private readonly WpmDbContext wpmDbContext;

    [BindProperty]
    public Pet Pet { get; set; }

    public SelectList Breeds { get; set; }

    public PetEditModel(WpmDbContext wpmDbContext)
    {
        this.wpmDbContext = wpmDbContext;
        var breeds = wpmDbContext.Breeds
        .Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();

        Breeds = new SelectList(breeds, "Value", "Text");
    }

    public void OnGet(int? id)
    {
        Pet = wpmDbContext.Pets
        .Where(p => p.Id == id)
        .First();
    }

    public IActionResult OnPost()
    {
        if (Pet != null)
        {
            wpmDbContext.Update(Pet);
            wpmDbContext.SaveChanges();
        }
        return RedirectToPage("Pets");
    }
}