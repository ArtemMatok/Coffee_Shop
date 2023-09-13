using Coffee_Shop.Data;
using Coffee_Shop.Models;
using Coffee_Shop.Service.Interfaces;
using Coffee_Shop.Service.Repository;
using Coffee_Shop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Coffee_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        

        public ProductController(IRepository<Product> productRepository, IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _productRepository = productRepository;

            _hostingEnvironment = hostingEnvironment;
            _context = context;
            Models.User.Basket = _context.Orders.ToList();
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAll(); 
            return View(products);
        }



        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
            };

            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = await _productRepository.GetById(id);
                if(vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm); 
                }

            }

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductVM vm, IFormFile? file)
        { 
             
                string fileName = String.Empty;
                if(file!=null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);

                    if(vm.Product.Image != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Product.Image.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var fileStream = new FileStream(filePath,FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    vm.Product.Image = @"\ProductImage\" + fileName;
                }
                
                    _productRepository.Add(vm.Product);
                    TempData["success"] = "Product Created Done!";
                
                
                return RedirectToAction("Index");
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),
            };
             vm.Product = await _productRepository.GetSmtById(id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }

            

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(ProductVM vm, IFormFile? file, int id)
        {
            vm.Product.Id = id;


            string fileName = String.Empty;
            if (file != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImage");
                fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                if (vm.Product.Image != null)
                {
                    var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Product.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                vm.Product.Image = @"\ProductImage\" + fileName;
            }
            
            
                _productRepository.Update(vm.Product);
                TempData["success"] = "Product Updated Done!";
            
            return RedirectToAction("Index");

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetSmtById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Product product)
        {
            var productDelete = await _productRepository.GetSmtById(id);
            _productRepository.Delete(productDelete);
            return RedirectToAction("Index");
        }



        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}
