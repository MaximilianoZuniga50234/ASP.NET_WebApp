using demowebapp.DAL;
using demowebapp.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace demowebapp.Pages;

public class PetsModel(WpmDbContext wpmDbContext) : PageModel
{
    private readonly WpmDbContext wpmDbContext = wpmDbContext;
    public IEnumerable<Pet> Pets { get; private set; }

    [BindProperty(SupportsGet = true)]
    public string Search { get; set; }

    public void OnGet()
    {
        Pets = wpmDbContext.Pets
        .Include(p => p.Breed)
        .ThenInclude(b => b.Species)
        .Where(p => string.IsNullOrWhiteSpace(Search) ||
        p.Name.Contains(Search, StringComparison.InvariantCultureIgnoreCase))
        .ToList();
    }
}

