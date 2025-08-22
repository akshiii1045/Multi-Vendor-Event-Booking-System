using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Multi_Vendor_Event_Booking_System.Data;
using Multi_Vendor_Event_Booking_System.Data.DTO;

namespace Multi_Vendor_Event_Booking_System.Validators
{
    public class Service_CatalogDTOValidator : AbstractValidator<Services_CatalogDTO>
    {
        private readonly EventDBContext _context;
        public Service_CatalogDTOValidator(EventDBContext context)
        {

            _context = context;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("A service with this title already exists.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(50).WithMessage("Category cannot be longer than 50 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.VendorId)
                .GreaterThan(0).WithMessage("VendorId must be a positive number.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

            RuleFor(x => x.isActive)
                .NotNull().WithMessage("isActive status must be specified.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return !await _context.Services_Catalogs.AnyAsync(s => s.Title.ToLower() == title.ToLower(), cancellationToken);
        }
    }
}
