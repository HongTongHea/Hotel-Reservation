using Hotel_Reservation.Data;
using Hotel_Reservation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Reservation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CustomerController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Index()
        {
            List<Customer> customers = _context.Customer.ToList();
            return View(customers);
        }

        public IActionResult Create()
        {
            Customer customer = new Customer();
            return PartialView("_CreateCustomer", customer);
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customer.Add(customer);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) here
                    ModelState.AddModelError("", "An error occurred while saving the customer.");
                }
            }

            // If validation fails, return the partial view with validation errors
            TempData["ShowCreateModal"] = true; // Flag to show the modal again
            return PartialView("_CreateCustomer", customer);
        }

        public IActionResult Edit(int id)
        {
            Customer customer = GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return PartialView("_EditCustomer", customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customer.Update(customer);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "User updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the exception (ex) here
                    ModelState.AddModelError("", "An error occurred while updating the customer.");
                }
            }

            // If validation fails, return the partial view with validation errors
            TempData["ShowEditModal"] = true; // Flag to show the modal again
            return PartialView("_EditCustomer", customer);
        }

        public IActionResult Delete(int id)
        {
            Customer customer = GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return PartialView("_DeleteCustomer", customer);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            Customer customer = GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            try
            {
                _context.Customer.Remove(customer);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "User deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                TempData["ErrorMessage"] = "An error occurred while deleting the customer.";
                return RedirectToAction("Index");
            }
        }

        private Customer GetCustomerById(int id)
        {
            return _context.Customer.FirstOrDefault(c => c.CustomerID == id);
        }
    }
}
