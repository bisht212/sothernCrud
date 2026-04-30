using System.Data;

namespace TravelAccomodationAPI.Shared.CommonMethods
{
    public class FileUploadCommon
    {
        //public async static Task<string> UploadFileAsync(IFormFile file)
        //{
        //    var folder = Path.Combine(Directory.GetCurrentDirectory(), "FileStorage", "Files");

        //    if (!Directory.Exists(folder))
        //        Directory.CreateDirectory(folder);

        //    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        //    var fullPath = Path.Combine(folder, fileName);

        //    using var stream = new FileStream(fullPath, FileMode.Create);
        //    await file.CopyToAsync(stream);

        //    return $"FileStorage/Files/{fileName}";
        //}

        public static async Task<string> UploadFileAsync(IFormFile file, object? entityType = null, object? entityId = null)
        {
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty");

                // Fixed storage folder
                var rootFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "FileStorage",
                    "Files"
                );

                if (!Directory.Exists(rootFolder))
                    Directory.CreateDirectory(rootFolder);

                // Convert dynamic values safely
                string entityTypeStr = entityType?.ToString() ?? string.Empty;
                string entityIdStr = entityId?.ToString() ?? string.Empty;

                // Original file parts
                var originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");

                // Build prefix: entityType_entityId
                string filePrefix = string.Join("_", entityTypeStr, entityIdStr)
                                    .Trim('_');

                // Build final filename
                var finalFileName = string.IsNullOrWhiteSpace(filePrefix)   ? $"{originalFileName}_{timestamp}{extension}" : $"{filePrefix}_{originalFileName}_{timestamp}{extension}";

                var fullPath = Path.Combine(rootFolder, finalFileName);

                // Save file
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // ✅ Return absolute path
                return Path.GetFullPath(fullPath);
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
