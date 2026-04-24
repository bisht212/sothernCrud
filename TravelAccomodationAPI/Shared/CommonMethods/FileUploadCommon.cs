using System.Data;

namespace TravelAccomodationAPI.Shared.CommonMethods
{
    public class FileUploadCommon
    {
        public async static Task<string> UploadFileAsync(IFormFile file)
        {
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "Files");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var fullPath = Path.Combine(folder, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"FileStorage/Files/{fileName}";
        }


        public static DataTable  ToDataTable<T>(IEnumerable<T> data)
        {
            var dt = new DataTable();

            var props = typeof(T).GetProperties();

            // Create columns
            foreach (var prop in props)
            {
                dt.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            // Add rows
            foreach (var item in data)
            {
                var values = props.Select(p => p.GetValue(item) ?? DBNull.Value).ToArray();
                dt.Rows.Add(values);
            }

            return dt;
        }
    }
}
