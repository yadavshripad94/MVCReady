using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepositoryPattern.Models;
using RepositoryPattern.Repository;

namespace RepositoryPattern.Controllers
{
    
        public class EmployeesController : Controller
        {
            //Other Than Employee Entity
            private readonly AppDbContext _context;

            //For Employee Entity
            private readonly IEmployeeRepository _employeeRepository;
            public EmployeesController(IEmployeeRepository employeeRepository, AppDbContext context)
            {
                _employeeRepository = employeeRepository;
                _context = context;
            }

            // GET: Employees
            public async Task<IActionResult> Index()
            {
                var employees = await _employeeRepository.GetAllAsync();
                return View(employees);
            }

            // GET: Employees/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = await _employeeRepository.GetByIdAsync(Convert.ToInt32(id));
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }

            // GET: Employees/Create
            public IActionResult Create()
            {
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
                return View();
            }

            // POST: Employees/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("EmployeeId,Name,Email,Position,DepartmentId")] Employee employee)
            {
                if (ModelState.IsValid)
                {
                    await _employeeRepository.InsertAsync(employee);

                    //Call SaveAsync to Insert the data into the database
                    await _employeeRepository.SaveAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
                return View(employee);
            }

            // GET: Employees/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = await _employeeRepository.GetByIdAsync(Convert.ToInt32(id));
                if (employee == null)
                {
                    return NotFound();
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
                return View(employee);
            }

            // POST: Employees/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,Name,Email,Position,DepartmentId")] Employee employee)
            {
                if (id != employee.EmployeeId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        await _employeeRepository.UpdateAsync(employee);
                        await _employeeRepository.SaveAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        var emp = await _employeeRepository.GetByIdAsync(employee.EmployeeId);
                        if (emp == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", employee.DepartmentId);
                return View(employee);
            }

            // GET: Employees/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var employee = await _employeeRepository.GetByIdAsync(Convert.ToInt32(id));
                if (employee == null)
                {
                    return NotFound();
                }

                return View(employee);
            }

            // POST: Employees/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee != null)
                {
                    await _employeeRepository.DeleteAsync(id);
                    await _employeeRepository.SaveAsync();
                }

                return RedirectToAction(nameof(Index));
            }
        
    }
}
