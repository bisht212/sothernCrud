using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Transactions;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.CommonMessage;
using TravelAccomodationAPI.Shared.CommonMethods;
using TravelAccomodationAPI.Shared.DBHelper;
using TravelAccomodationAPI.Shared.Enums;
using TravelAccomodationAPI.Shared.StoredProcedures;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelAccomodationAPI.BusinessClass
{
    public class HotelMaster_Business : IHotelMaster_Business
    {

        private readonly IDataAccess _da;
        private readonly DbContext _context;
        public HotelMaster_Business(IDataAccess da, DbContext context)
        {
            _da = da;
            _context = context;
        }

        public async Task<IEnumerable<HotelMasterResponse>> GetHotelMasterList(HotelFilterRequest hotelFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenantID", hotelFilter.TenantId);
            parameters.Add("@hotel_name", hotelFilter.HotelName);

            var result = await _da.GetListAsync<HotelMasterResponse>(
                Stored_Procedures.GET_HOTEL_MASTER, parameters);

            if (result == null)
            {
                throw new ApiException(
                    string.Format(ErrorMessage.HOTEL_NOT_FOUND, hotelFilter.TenantId), 404);
            }

            return result;
        }

        public async Task<AddHotelMasterResponse> AddhotelsMaster(AddHotelMasterRequest hotel)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@TenantID", hotel.TeanatId);
            parameters.Add("@hotel_name", hotel.Hotel_Name);
            parameters.Add("@propertyType_id", hotel.Property_Type_Id);
            parameters.Add("@starRating_id", hotel.Star_Rating_Id);

            parameters.Add("@owner_name", hotel.Owner_Name);
            parameters.Add("@owner_phone", hotel.Owner_Phone);

            parameters.Add("@chainstand_id", hotel.ChainStandId);

            parameters.Add("@address_line1", hotel.AddressLine1);
            parameters.Add("@address_line2", hotel.AddressLine2);

            parameters.Add("@city", hotel.City);
            parameters.Add("@state", hotel.State);
            parameters.Add("@country", hotel.Country);
            parameters.Add("@pin_code", hotel.PinCode);

            parameters.Add("@landmark", hotel.LandMark);

            parameters.Add("@year_of_construction", hotel.Year_of_construction);
            parameters.Add("@number_of_floors", hotel.number_of_floors);
            parameters.Add("@number_of_rooms", hotel.number_of_rooms);

            parameters.Add("@check_in_time", hotel.Check_In_Time);
            parameters.Add("@check_out_time", hotel.Check_Out_Time);

            parameters.Add("@how_to_reach", hotel.How_To_Reach);

            parameters.Add("@google_map_link", hotel.Google_Map_Link);
            parameters.Add("@google_hotel_link", hotel.Google_Hotel_Link);
            parameters.Add("@trip_advisor_link", hotel.Trip_Advisor_Link);

            parameters.Add("@distance_from_airport", hotel.Distance_From_Airport);
            parameters.Add("@distance_from_railway", hotel.Distance_From_Railway);
            parameters.Add("@distance_from_isbt", hotel.Distance_From_ISBT);

            parameters.Add("@created_by", hotel.Created_By);

            var response = await _da.GetAsync<AddHotelMasterResponse>(
                Stored_Procedures.ADD_HOTEL_MASTER,
                parameters);

            if (response == null)
            {
                throw new ApiException(ErrorMessage.HOTEL_ADD_FAILES, Convert.ToInt32(StatusCode.Badrequest));
            }

            return response;
        }

        public async Task<IEnumerable<GetRestaurantResponse>> GetRestaurantDetail(long hotelId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@HotelId", hotelId);


            var result = await _da.GetListAsync<GetRestaurantResponse>(
                Stored_Procedures.GET_RESTURANT_LIST, parameters);

            return result.ToList();
        }

        //public async Task InsertRestaurantsWithFiles(List<AddRestaurantsOnPropertyRequest> request)
        //{
        //    //using var connection = _context.GetConnection();
        //    //using var transaction = connection.BeginTransaction();

        //    //try
        //    //{
        //        foreach (var item in request)
        //        {
        //            var param = new DynamicParameters();
        //            param.Add("@Hotel_Id", item.Hotel_Id);
        //            param.Add("@Resta_Name", item.Resta_Name);
        //            param.Add("@Veg_Id", item.Veg_Id);
        //            param.Add("@Cuisine_Id", item.Cuisine_Id);
        //            param.Add("@No_of_covers", item.No_of_covers);
        //            param.Add("@In_room_dining_facility", item.In_room_dining_facility);

        //            var restaId = await _da.ExecuteScalarAsync<int>(
        //                Stored_Procedures.ADD_BULK_RESTURANTPROPERTY,
        //                param
        //            );
        //        if(restaId > 0) {
        //            if (item.ResturantImage != null && item.ResturantImage.Any())
        //            {
        //                foreach (var file in item.ResturantImage)
        //                {
        //                    var filePath = await FileUploadCommon.UploadFileAsync(file);

        //                    var fileParam = new DynamicParameters();
        //                    fileParam.Add("@HotelId", item.Hotel_Id);
        //                    fileParam.Add("@RestaurantId", restaId);
        //                    fileParam.Add("@FilePath", filePath);
        //                    fileParam.Add("@CreatedBy", "Admin");

        //                    await _da.ExecuteAsync(
        //                        Stored_Procedures.UPLOAD_FILE_SP,
        //                        fileParam
        //                    );
        //                }
        //            }
        //        }


        //        }

        //    //    transaction.Commit();
        //    //}
        //    //catch
        //    //{
        //    //    transaction.Rollback();
        //    //    throw;
        //    //}
        //}

        // Optimize this using TVP future
        public async Task InsertRestaurantsWithFiles(List<AddRestaurantsOnPropertyRequest> request)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in request)
                        {
                            var param = new DynamicParameters();
                            param.Add("@Hotel_Id", item.Hotel_Id);
                            param.Add("@Resta_Name", item.Resta_Name);
                            param.Add("@Veg_Id", item.Veg_Id);
                            param.Add("@Cuisine_Id", item.Cuisine_Id);
                            param.Add("@No_of_covers", item.No_of_covers);
                            param.Add("@In_room_dining_facility", item.In_room_dining_facility);

                            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                                Stored_Procedures.ADD_BULK_RESTURANTPROPERTY,
                                param,
                                transaction,
                                commandType: CommandType.StoredProcedure
                            );

                            int restaId = result?.Status ;

                            if (restaId <= 0)
                                throw new ApiException("Restaurant already exists", Convert.ToInt32(StatusCode.Conflict));

                            // Insert Files
                            if (item.ResturantImage != null && item.ResturantImage.Any())
                            {
                                foreach (var file in item.ResturantImage)
                                {
                                    var filePath = await FileUploadCommon.UploadFileAsync(file , item.Hotel_Id, restaId);

                                    var fileParam = new DynamicParameters();
                                    fileParam.Add("@HotelId", item.Hotel_Id);
                                    fileParam.Add("@RestaurantId", restaId);
                                    fileParam.Add("@FilePath", filePath);
                                    fileParam.Add("@CreatedBy", "Admin");

                                    await connection.ExecuteAsync(
                                        Stored_Procedures.UPLOAD_FILE_SP,
                                        fileParam,
                                        transaction,
                                        commandType: CommandType.StoredProcedure
                                    );
                                }
                            }
                        }

                        //Commit only if ALL succeed
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        //  Rollback everything if any error
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
      
        public async Task UpdateRestaurantsWithFiles(int rest_Id, AddRestaurantsOnPropertyRequest request)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var uploadedFiles = new List<string>();

                    try
                    {
                        //foreach (var item in request)
                        //{
                        // 1. Update Restaurant
                        var updateParam = new DynamicParameters();
                        updateParam.Add("@RestaurantId", rest_Id);
                        updateParam.Add("@Hotel_Id", request.Hotel_Id);
                        updateParam.Add("@Resta_Name", request.Resta_Name);
                        updateParam.Add("@Veg_Id", request.Veg_Id);
                        updateParam.Add("@Cuisine_Id", request.Cuisine_Id);
                        updateParam.Add("@No_of_covers", request.No_of_covers);
                        updateParam.Add("@In_room_dining_facility", request.In_room_dining_facility);

                        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                            Stored_Procedures.UPDATE_RESTURANTPROPERTY,
                            updateParam,
                            transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        int restaurantId = result?.Status ?? 0;

                        if (restaurantId <= 0)
                            throw new ApiException(result.Message, 400);

                        // 🔹 2. Remove old files (Soft delete)
                        var deleteParam = new DynamicParameters();
                        deleteParam.Add("@HotelId", request.Hotel_Id);
                        deleteParam.Add("@RestaurantId", restaurantId);

                        await connection.ExecuteAsync(
                            Stored_Procedures.UPDATE_RESTURANTFILE,
                            deleteParam,
                            transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        // 🔹 3. Upload & Insert new files
                        if (request.ResturantImage != null && request.ResturantImage.Any())
                        {
                            foreach (var file in request.ResturantImage)
                            {
                                var filePath = await FileUploadCommon.UploadFileAsync(file, request.Hotel_Id, rest_Id);
                                uploadedFiles.Add(filePath);

                                var fileParam = new DynamicParameters();
                                fileParam.Add("@HotelId", request.Hotel_Id);
                                fileParam.Add("@RestaurantId", restaurantId);
                                fileParam.Add("@FilePath", filePath);
                                fileParam.Add("@CreatedBy", "Admin");

                                await connection.ExecuteAsync(
                                    Stored_Procedures.UPLOAD_FILE_SP,
                                    fileParam,
                                    transaction,
                                    commandType: CommandType.StoredProcedure
                                );
                            }
                        }
                        //   }

                        // ✅ Commit if all success
                        transaction.Commit();
                    }
                    catch
                    {
                        // ❌ Rollback DB
                        transaction.Rollback();

                        // ❌ Delete uploaded files
                        foreach (var file in uploadedFiles)
                        {
                            try
                            {
                                if (File.Exists(file))
                                    File.Delete(file);
                            }
                            catch { }
                        }

                        throw;
                    }
                }
            }
        }

        public async Task<dynamic> DeleteRestaurant(int rest_Id)
        {
            var param = new DynamicParameters();
            param.Add("@resta_id", rest_Id);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELETE_RESTAURANT,
                param
            );

            if (result?.Status <= 0)
            {
                throw new ApiException(result?.Message ?? "Error", 400);
            }

            return result;
        }

        public async Task AddHotelContacts(List<AddHotelContacts> hotelContacts)
        {
            int restaId = 0;
            foreach (var item in hotelContacts)
            {
                var param = new DynamicParameters();
                param.Add("@Name", item.Name);
                param.Add("@Hotel_Id", item.Hotel_Id);
                param.Add("@Department", item.Department);
                param.Add("@Designation", item.Designation);
                param.Add("@landline_country_code", item.Landline_Country_Code);
                param.Add("@landline_number", item.Landline_Number);
                param.Add("@whatsapp_country_code", item.Whatsapp_Country_Code);
                param.Add("@whatsapp_number", item.Whatsapp_Country_Code);
                param.Add("@created_by", "Admin");

                var result = await _da.ExecuteWithResponseAsync<dynamic>(
                    Stored_Procedures.ADD_BULK_HOTEL_CONTACTS,
                    param
                );

                restaId = result.Status;
                if (restaId <= 0)
                {
                    throw new ApiException(result.Message, Convert.ToInt32(StatusCode.Badrequest));
                }

            }

        }
     
        public async Task UpdateHotelContacts(int ContactId, AddHotelContacts hotelContacts)
        {

            var param = new DynamicParameters();
            param.Add("@contact_id", ContactId); // ✅ NEW
            param.Add("@name", hotelContacts.Name);
            param.Add("@department", hotelContacts.Department);
            param.Add("@designation", hotelContacts.Designation);
            param.Add("@landline_country_code", hotelContacts.Landline_Country_Code);
            param.Add("@landline_number", hotelContacts.Landline_Number);
            param.Add("@whatsapp_country_code", hotelContacts.Whatsapp_Country_Code);
            param.Add("@whatsapp_number", hotelContacts.Whatsapp_Number);
            param.Add("@updated_By", "Admin");
            //  param.Add("@updated_", "Admin");


            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                    Stored_Procedures.UPDATE_BULK_HOTEL_CONTACTS,
                    param
                );

            if (result.Status <= 0)
            {
                throw new ApiException(result.Message, Convert.ToInt32(StatusCode.Badrequest));
            }

        }

        public async Task<dynamic> DeleteHotelContact(int contact_id)
        {
            var param = new DynamicParameters();
            param.Add("@contact_id", contact_id);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELTE_HOTEL_CONTACT,
                param
            );

            if (result?.Status <= 0)
            {
                throw new ApiException(result?.Message ?? "Error", 400);
            }

            return result;
        }
     
        public async Task AddHotelContactPhoneNumber(AddHotelContactPhoneNumberRequest phoneNumberRequest)
        {

            var parameters = new DynamicParameters();

            parameters.Add("@contact_id", phoneNumberRequest.ContactId, DbType.Int64);
            parameters.Add("@country_code", phoneNumberRequest.CountryCode, DbType.String);
            parameters.Add("@phone_number", phoneNumberRequest.Phone_Number, DbType.String);
            parameters.Add("@phonetype_id", phoneNumberRequest.PhoneType_Id, DbType.Int32);
            parameters.Add("@created_by", phoneNumberRequest.Created_By ?? "Admin");

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.ADD_HOTEL_CONTACT_PHONE_NUMBER,
                parameters
            );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }

        }

        public async Task<dynamic> DeletePhoneId(long phone_Id)
        {

            var param = new DynamicParameters();
            param.Add("@phone_id", phone_Id);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELETE_HOTEL_CONTACT_PHONE_NUMBER,
                param
            );

            if (result?.Status <= 0)
            {
                throw new ApiException(result?.Message ?? "Error", 404);
            }

            return result;
        }

        public async Task AddHotelContactEmail(AddHotelContactEmailRequest emailRequest)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@contact_id", emailRequest.ContactId, DbType.Int64);
            parameters.Add("@email", emailRequest.Email, DbType.String);
            parameters.Add("@created_by", emailRequest.Created_By ?? "Admin");

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.ADD_HOTEl_CONTACT_EMAIL,
                parameters
            );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }

        }

        public async Task<dynamic> DeleteEmailId(long email_Id)
        {
            var param = new DynamicParameters();
            param.Add("@email_id", email_Id);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELETE_HOTEL_CONTTACT_EMAIL,
                param
            );

            if (result?.Status <= 0)
            {
                throw new ApiException(result?.Message ?? "Error", 404);
            }

            return result;
        }
       
        public async Task<IEnumerable<HotelFacilityCategoryResponse>> GetHotelFacilityCategory()
        {
            IEnumerable<HotelFacilityCategoryResponse> result = await _da.GetListAsync<HotelFacilityCategoryResponse>(
                Stored_Procedures.GET_HOTEL_FACILITY_CATEGORIES);

            return result.ToList();
        }
        
        public async Task<HotelFacilityCategoryResponse> GetHotelFacilityCategoryByID(int facility_CategoryId)
        {
            var param = new DynamicParameters();
            param.Add("@Facility_CategoryId", facility_CategoryId);

            HotelFacilityCategoryResponse result = await _da.GetAsync<HotelFacilityCategoryResponse>(
               Stored_Procedures.GET_HOTEL_FACILITY_CATEGORYID, param);

            return result;
        }

        public async Task<IEnumerable<GetBanquetResponse>> GetBanqueteByHotelId(long hotelId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@HotelId", hotelId);


            var result = await _da.GetListAsync<GetBanquetResponse>(
                Stored_Procedures.GET_BANQUETS, parameters);

            return result.ToList();
        }
     
        public async Task AddHotelFacility(List<AddHotelFacilitiesRequest> hotelFacilityRequest)
        {

            foreach (var item in hotelFacilityRequest)
            {
                var param = new DynamicParameters();
                param.Add("@hotel_id", item.Hotel_Id);
                param.Add("@AmenityID", item.AminittyId);
                param.Add("@created_by", item.Created_By);

                await _da.ExecuteAsync(
                    Stored_Procedures.ADD_HOTEL_FACILITIES,
                    param

                );

            }

        }

        public async Task InsertBanquetWithFiles(List<AddBanquestRequest> request)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in request)
                        {
                            // 1. Insert Banquet
                            var param = new DynamicParameters();
                            param.Add("@Hotel_Id", item.Hotel_Id);
                            param.Add("@Banquet_Name", item.Banquet_Name);
                            param.Add("@No_of_covers", item.No_of_covers);
                            param.Add("@Per_plate_cost_veg", item.Per_plate_cost_veg);
                            param.Add("@Per_plate_cost_non_veg", item.Per_plate_cost_non_veg);
                            param.Add("@Sq_feet_area", item.Sq_feet_area);
                            param.Add("@Description", item.Description);

                            var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                                Stored_Procedures.ADD_BULK_BANQUET,
                                param,
                                transaction,
                                commandType: CommandType.StoredProcedure
                            );

                            int banquetId = result.Status;

                            if (banquetId <= 0)
                                throw new ApiException(
                                    result.Message,
                                    Convert.ToInt32(StatusCode.Conflict)
                                );

                            // 2. Insert Banquet Images
                            if (item.BanquetImages != null && item.BanquetImages.Any())
                            {
                                foreach (var file in item.BanquetImages)
                                {
                                    var filePath =
                                        await FileUploadCommon.UploadFileAsync(file, item.Hotel_Id, banquetId);

                                    var fileParam = new DynamicParameters();
                                    fileParam.Add("@HotelId", item.Hotel_Id);
                                    fileParam.Add("@BanquetId", banquetId);
                                    fileParam.Add("@FilePath", filePath);
                                    fileParam.Add("@CreatedBy", "Admin");

                                    await connection.ExecuteAsync(
                                        Stored_Procedures.UPLOAD_BANQUET_FILES,
                                        fileParam,
                                        transaction,
                                        commandType: CommandType.StoredProcedure
                                    );
                                }
                            }
                        }

                        // ✅ Commit only if ALL inserts succeed
                        transaction.Commit();
                    }
                    catch
                    {
                        // ❌ Rollback everything if any failure
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public async Task UpdateBanquetWithFiles(int banquetId, AddBanquestRequest request)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var uploadedFiles = new List<string>();

                    try
                    {
                        // 🔹 1. Update Banquet
                        var updateParam = new DynamicParameters();
                        updateParam.Add("@BanquetId", banquetId);
                        updateParam.Add("@Hotel_Id", request.Hotel_Id);
                        updateParam.Add("@Banquet_Name", request.Banquet_Name);
                        updateParam.Add("@No_of_covers", request.No_of_covers);
                        updateParam.Add("@Per_plate_cost_veg", request.Per_plate_cost_veg);
                        updateParam.Add("@Per_plate_cost_non_veg", request.Per_plate_cost_non_veg);
                        updateParam.Add("@Sq_feet_area", request.Sq_feet_area);
                        updateParam.Add("@Description", request.Description);

                        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                            "usp_Update_Banquet",
                            updateParam,
                            transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        int updatedBanquetId = result?.Status ?? 0;

                        if (updatedBanquetId <= 0)
                            throw new ApiException(result?.Message ?? "Banquet update failed", 400);

                        // 🔹 2. Soft delete existing banquet files
                        var deleteParam = new DynamicParameters();
                        deleteParam.Add("@HotelId", request.Hotel_Id);
                        deleteParam.Add("@BanquetId", updatedBanquetId);

                        await connection.ExecuteAsync(
                            "sp_ReplaceBanquetFiles",
                            deleteParam,
                            transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        // 🔹 3. Upload & insert new banquet files
                        if (request.BanquetImages != null && request.BanquetImages.Any())
                        {
                            foreach (var file in request.BanquetImages)
                            {
                                var filePath = await FileUploadCommon.UploadFileAsync(file, request.Hotel_Id, updatedBanquetId);
                                uploadedFiles.Add(filePath);

                                var fileParam = new DynamicParameters();
                                fileParam.Add("@HotelId", request.Hotel_Id);
                                fileParam.Add("@BanquetId", updatedBanquetId);
                                fileParam.Add("@FilePath", filePath);
                                fileParam.Add("@CreatedBy", "Admin");

                                await connection.ExecuteAsync(
                                    Stored_Procedures.UPLOAD_BANQUET_FILE_SP,
                                    fileParam,
                                    transaction,
                                    commandType: CommandType.StoredProcedure
                                );
                            }
                        }

                        // ✅ Commit if everything succeeds
                        transaction.Commit();
                    }
                    catch
                    {
                        // ❌ Rollback DB changes
                        transaction.Rollback();

                        // ❌ Cleanup uploaded files
                        foreach (var file in uploadedFiles)
                        {
                            try
                            {
                                if (File.Exists(file))
                                    File.Delete(file);
                            }
                            catch { }
                        }

                        throw;
                    }
                }
            }
        }

        public async Task<dynamic> DeleteBanquete(long banquete_Id)
        {
            var param = new DynamicParameters();
            param.Add("@banquet_id", banquete_Id);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELETE_BANQUET,
                param
            );

            if (result?.Status <= 0)
            {
                throw new ApiException(result?.Message ?? "Error", 400);
            }

            return result;
        }

        public async Task<dynamic> BulkUploadHotelFilesAsync(List<AddHotelFilesRequest> files)
        {

            if (files == null || files.Count == 0)
                throw new ArgumentException("No files received.");

            var table = CreateHotelFilesDataTable();

            foreach (var item in files)
            {
                if (item.FIle == null || item.FIle.Length == 0)
                    throw new ArgumentException("No files received.");

                // ✅ CENTRALIZED FILE UPLOAD
                var filePath = await FileUploadCommon.UploadFileAsync(item.FIle);

                table.Rows.Add(
                    item.HotelId,
                    item.FileName ?? item.FileName,
                    item.Description,
                    filePath,                 // ✅ Generated path
                    "Admin"
                );
            }

            if (table.Rows.Count == 0)
                throw new InvalidOperationException("No valid files to insert.");

            var parameters = new DynamicParameters();
            parameters.Add(
                "@HotelFiles",
                table.AsTableValuedParameter("dbo.HotelFiles_TVP")
            );

            var response  = await _da.ExecuteWithResponseAsync<dynamic>(
               Stored_Procedures.ADD_HOTEL_FILES,
                parameters);


            // 🔴 DUPLICATE FILE CASE
            if (response.Status == 0)
            {
                throw new ApiException(response.Message, Convert.ToInt32(StatusCode.Badrequest));
            }

            return response;

        }
       
        public async Task<dynamic> UpdateHotelFIle(long fileId, AddHotelFilesRequest file)
        {
            if (file.FIle == null || file.FIle.Length == 0)
                throw new ArgumentException("No files received.");

            // ✅ CENTRALIZED FILE UPLOAD
            var filePath = await FileUploadCommon.UploadFileAsync(file.FIle, file.HotelId);

            var parameters = new DynamicParameters();
            parameters.Add("@FileId", fileId);
            parameters.Add("@HotelId", file.HotelId);
            parameters.Add("@FileName", file.FileName);
            parameters.Add("@Description", file.Description);
            parameters.Add("@FilePath", filePath);
            parameters.Add("@UpdatedBy", "Admin");


            var response = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.UPDATE_HOTEL_FILE,
                parameters);

            if (response.Status == 0)
            {
                throw new ApiException(response.Message, Convert.ToInt32(StatusCode.Badrequest));
            }

            return response; 
        }

        public async Task<dynamic> DeleteHotelFile(long fileId, long hotelId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FileId", fileId);
            parameters.Add("@HotelId", hotelId);
            parameters.Add("@UpdatedBy", "Admin");

            var response = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELETE_HOTEL_FILE,
                parameters);

            if (response.Status == 0)
            {
                throw new InvalidOperationException(response.Message);
            }

            return response;
        }

        public async Task<IEnumerable<GetHotelFilesResponse>> GetHotelFiles(long hotelId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@HotelId", hotelId);


            var result = await _da.GetListAsync<GetHotelFilesResponse>(
                Stored_Procedures.GET_HOTEL_FILES, parameters);

            return result.ToList();
        }

        private DataTable CreateHotelFilesDataTable()
        {
            var table = new DataTable();

            table.Columns.Add("HotelId", typeof(long));
            table.Columns.Add("FileName", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("FilePath", typeof(string));
         //   table.Columns.Add("Created_At", typeof(DateTime));
            table.Columns.Add("Created_By", typeof(string));

            return table;
        }

        public async Task<dynamic> HotelMasterAsDraft(long hotelId, bool isDraft, string updatedBy)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@hotel_id", hotelId);
            parameters.Add("@IsDraft", isDraft);
            parameters.Add("@updated_by", updatedBy);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
               Stored_Procedures.HOTELMASTER_SAVE_AS_DRAFT, parameters);

            if (result.Status <= 0) {
                throw new ApiException(result.Message, Convert.ToInt32(StatusCode.Badrequest));
            }

            return result; 
        }

        public async Task AddRoom(List<AddRoomDetailsRequest> roomDetailRequest)
        {


            using var connection = _context.GetConnection();
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                if (roomDetailRequest.Count == 0)
                {
                    throw new ApiException("Field is required", Convert.ToInt32(StatusCode.Conflict));
                }

                foreach (var room in roomDetailRequest)
                {
                    // 1️⃣ Insert Room
                    var roomParams = new DynamicParameters();
                    roomParams.Add("@HotelId", room.RoomRequest.HotelId);
                    roomParams.Add("@RoomCategoryName", room.RoomRequest.RoomCategoryName);
                    roomParams.Add("@RoomDescription", room.RoomRequest.RoomDescription);
                    roomParams.Add("@NoOfBeds", room.RoomRequest.NoOfBeds);
                    roomParams.Add("@NoOfRooms", room.RoomRequest.NoOfRooms);

                    int roomId = await connection.QueryFirstOrDefaultAsync<int>(
                        Stored_Procedures.ADD_HOTEL_ROOMS,
                        roomParams,
                        transaction
                    );

                    // 2️⃣ Bulk Insert Room Media
                    if (room.RoomMediaRequste != null && room.RoomMediaRequste.Any())
                    {
                        foreach (var media in room.RoomMediaRequste)
                        {
                            var filePath = await FileUploadCommon.UploadFileAsync(media.ImagePath, room.RoomRequest.HotelId, roomId);

                            var mediaParams = new DynamicParameters();
                            mediaParams.Add("@RoomId", roomId);
                            mediaParams.Add("@ImageName", media.ImageName);
                            mediaParams.Add("@Description", media.Description);
                            mediaParams.Add("@ImageData", filePath);

                            await connection.ExecuteAsync(
                                Stored_Procedures.ADD_ROOM_MEDIA,
                                mediaParams,
                                transaction
                            );
                        }
                    }

                    // 3️⃣ Bulk Insert Room Facilities
                    if (room.RoomFacilityRequeste != null && room.RoomFacilityRequeste.Any())
                    {
                        foreach (var facility in room.RoomFacilityRequeste)
                        {
                            var facilityParams = new DynamicParameters();
                            facilityParams.Add("@RoomId", roomId);
                            facilityParams.Add("@RoomFacilityId", facility.RoomFacilityId);

                            await connection.ExecuteAsync(
                                Stored_Procedures.ADD_ROOM_FACILITY,
                                facilityParams,
                                transaction
                            );
                        }
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }


        }
    }
}