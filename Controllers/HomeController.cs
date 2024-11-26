using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormsApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FormsApp.Controllers;

public class HomeController : Controller
{
    
    public HomeController()
    {
    
    }

    public IActionResult Index(string searchString, string category)
    {
        var products = Repository.Product;

        if(!String.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            products = products.Where(p => p.Name.ToLower().Contains(searchString)).ToList();
        }

        if (!String.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

       // ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name",category);
       var model = new ProductViewModel{
        Products = products,
        Categories = Repository.Categories,
        SelectedCategory = category
       };
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile) //girdiğim formdaki bilgileri ekletirim.
    {
        var extension="";

        if (imageFile != null)
        {
             var allowedExtensions = new[]{".jpg",".jpeg",".png"};
             extension = Path.GetExtension(imageFile.FileName); //abc.jpg
       
        
            if(!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("","Geçerli bir resim seçiniz.");

            }
        }

        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img",imageFile.FileName); //resmin yüklenmesi
                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await imageFile!.CopyToAsync(stream);
                }
            }
           
            model.ProductId = Repository.Product.Count +1;
             Repository.CreateProduct(model);
             return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
       
    }
    public IActionResult Edit(int? id) //edit formu
    {
        if (id == null)
        {
            return NotFound();
        }

        var entity = Repository.Product.FirstOrDefault(p => p.ProductId ==id);
        if (entity == null)
        {
            return NotFound();
        }
         ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(entity);
    }

    [HttpPost] //editi kaydetme
    public async Task<IActionResult> Edit(int id, Product model,IFormFile?imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            if (imageFile != null)
            {
                var allowedExtensions = new[]{".jpg",".jpeg",".png"};
                var extension = Path.GetExtension(imageFile.FileName); //abc.jpg

                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img",imageFile.FileName); //resmin yüklenmesi

                using(var stream = new FileStream(path,FileMode.Create))
                {
                   await imageFile!.CopyToAsync(stream);
                }
                model.Image = randomFileName;
            }
            Repository.EditProduct(model);
            return RedirectToAction("Index");
        }
        
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }

    public IActionResult Delete(int? id) //ürün silme
    {
        if (id==null)
        {
            return NotFound();
        }
        var entity = Repository.Product.FirstOrDefault(p => p.ProductId ==id);
        if (entity == null)
        {
            return NotFound();
        }
        return View("DeleteConfirm",entity);

    }
    [HttpPost]
    public IActionResult Delete(int id, int ProductId) //ürün silme
    {
        if (id != ProductId)
        {
            return NotFound();
        }
        var entity = Repository.Product.FirstOrDefault(p => p.ProductId == ProductId);

        if (entity == null)
        {
            return NotFound();
        }

        Repository.DeleteProduct(entity);

        return RedirectToAction("Index");
    }

}
