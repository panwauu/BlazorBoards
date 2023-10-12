using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Services;

public class LabelService
{
    private readonly BoardContext _context;

    public LabelService(BoardContext context)
    {
        _context = context;
    }

    public List<Models.LabelDB> GetLabels()
    {
        return _context.Labels.AsNoTracking().ToList();
    }

    public Models.LabelDB? GetLabelById(int id)
    {
        return _context.Labels.AsNoTracking().FirstOrDefault(l => l.Id == id);
    }
}

