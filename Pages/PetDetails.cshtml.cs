using demowebapp.DAL;
using demowebapp.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace demowebapp.Pages;

public class PetDetailsModel(WpmDbContext wpmDbContext) : PageModel
{
    private readonly WpmDbContext wpmDbContext = wpmDbContext;

    public Pet Pet { get; set; }
    public void OnGet(int? id)
    {
        var pet = wpmDbContext.Pets
        .Where(p => p.Id == id)
        .Include(p => p.Owners)
        .Include(p => p.Breed)
        .ThenInclude(b => b.Species)
        .First();

        Pet = pet;
    }
}

