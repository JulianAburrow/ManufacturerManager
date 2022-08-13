using ManufacturerManager.DataAccess;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace ManufacturerManager.FrontEnd.Classes
{
    public class ManufacturerManagerPageModel : PageModel
    {
        protected ManufacturerManagerContext _context;

        public ManufacturerManagerPageModel(ManufacturerManagerContext context)
        {
            _context = context;
            _context.UserId = GetUserId();
        }

        #region GetUserId

        private int GetUserId()
        {
            var hour = DateTime.Now.Hour;
            int returnValue;
            if (hour % 3 == 0)
            {
                returnValue = 3;
            }
            else if (hour % 2 == 0)
            {
                returnValue = 2;
            }
            else
            {
                returnValue = 1;
            }
            return returnValue; ;
        }

        #endregion
    }
}


