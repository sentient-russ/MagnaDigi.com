using magnadigi.Models;
using Microsoft.AspNetCore.Hosting;


namespace magnadigi.Services
{
	
	public class FileSaver
	{
        IWebHostEnvironment _env;

		public FileSaver()
		{
		}

		public FileSaver(IWebHostEnvironment env)
		{
			_env = env;
		}
		public async Task FileSaveAsync(IFormFile file, string filePath, string UserName, string Project)
		{
			
			//create file name
			string date = DateTime.Now.ToString("yyyyMMddHHmmss");
			date = date + "-key-";
            string filename = $"{date}{file.FileName}";
            string route = Path.Combine(_env.WebRootPath, filePath);
			if (!Directory.Exists(route))
			{
				Directory.CreateDirectory(route);
			}
			string fileRoute = Path.Combine(route, filename);
			using (FileStream fs = File.Create(fileRoute))
			{
				await file.OpenReadStream().CopyToAsync(fs);
			}
			//add details to database
			string FileName = file.FileName;
			string FilePath = filename;
			string FileUploadedBy = UserName;
			string ProjectId = Project;
			FilesDAO database = new FilesDAO();
			List<FilesUploadedModel> result = database.WriteRecord(FileName, FilePath, FileUploadedBy, ProjectId);
		}
        public async Task WeddingFileSaveAsync(IFormFile file, string filePath, string UserName, string Project)
        {

            //create file name
            string date = DateTime.Now.ToString("yyyyMMddHHmmss");
            date = date + "-key-";
            string filename = $"{date}{file.FileName}";
            string route = Path.Combine(_env.WebRootPath, filePath);
            if (!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }
            string fileRoute = Path.Combine(route, filename);
            using (FileStream fs = File.Create(fileRoute))
            {
                await file.OpenReadStream().CopyToAsync(fs);
            }
            //add details to database
            string FileName = file.FileName;
            string FilePath = filename;
            string FileUploadedBy = UserName;
            string ProjectId = Project;
            FilesDAO database = new FilesDAO();
            List<FilesUploadedModel> result = database.WriteRecord(FileName, FilePath, FileUploadedBy, ProjectId);
        }
    }
}

