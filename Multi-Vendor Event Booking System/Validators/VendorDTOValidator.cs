using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Multi_Vendor_Event_Booking_System.Data;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Validators
{
    public class VendorDTOValidator : AbstractValidator<VendorDTO>
    {
        private readonly EventDBContext _context;
        public VendorDTOValidator(EventDBContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is Required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email format.")
                .MustAsync(UniqueEmail).WithMessage("An email exists with same email.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[0-9]{10}$").WithMessage("Phone number must be 10 digits");

        }

        public async Task<bool> UniqueEmail(string email,CancellationToken cancellationToken)
        {
            return !await _context.Vendors.AnyAsync(e => e.Email.ToLower() == email.ToLower());
        }
    }
}
