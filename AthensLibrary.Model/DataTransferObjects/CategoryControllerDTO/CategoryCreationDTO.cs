using AthensLibrary.Model.Helpers.HelperClasses;

namespace AthensLibrary.Model.DataTransferObjects.CategoryControllerDTO
{
    public class CategoryCreationDTO : TimeStamp
    {
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
