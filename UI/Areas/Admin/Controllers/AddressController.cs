using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
  public class AddressController : BaseController
  {
    public readonly AddressBLL bll = new AddressBLL();


    public ActionResult AddressList()
    {
      List<AddressDTO> model = bll.GetAddress();
      return View(model);
    }
    public ActionResult AddAddress()
    {
      AddressDTO dto = new AddressDTO();
      return View(dto);
    }

    [HttpPost]
    public ActionResult AddAddress(AddressDTO model)
    {
      if (ModelState.IsValid)
      {
        if (bll.AddAddress(model))
        {
          ViewBag.ProcessState = General.Messages.AddSuccess;
          ModelState.Clear();
          model = new AddressDTO();
        }
        else
        {
          ViewBag.ProcessState = General.Messages.GeneralError;
        }
      }
      else
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }

      return View(model);
    }

    public ActionResult UpdateAddress(int ID)
    {
      AddressDTO model = new AddressDTO();
      model = bll.GetAddressID(ID);
      return View(model);
    }

    [HttpPost]
    public ActionResult UpdateAddress(AddressDTO model)
    {
      if (ModelState.IsValid)
      {
        if (bll.UpdateMeta(model))
        {
          ViewBag.ProcessState = General.Messages.UpdateSuccess;
        }
        else
        {
          ViewBag.ProcessState = General.Messages.GeneralError;
        }
      }

      else
      {
        ViewBag.ProcessState = General.Messages.EmptyArea;
      }

      return View(model);
    }

    public JsonResult DeleteAddress(int ID)
    {
      bll.DeleteAddress(ID);
        return Json("");
    }


  }
}