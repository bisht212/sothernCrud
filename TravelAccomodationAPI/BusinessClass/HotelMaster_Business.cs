using Dapper;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Data;
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

        public async Task<IEnumerable<GetVeg_NonVegResponse>> GetAll_Veg_Non_Veg()
        {
            var result = await _da.GetListAsync<GetVeg_NonVegResponse>(
                Stored_Procedures.GET_ALL_VEG_NON_VEG);

            return result;
        }

        public async Task<GetVeg_NonVegResponse> Get_Veg_Non_Veg(int Id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@veg_id", Id);
            var result = await _da.GetAsync<GetVeg_NonVegResponse>(
                  Stored_Procedures.GET_VEG_NON_VEG, parameters);

            return result;
        }

        public async Task<IEnumerable<GetCuisine>> GetAll_Cuisine()
        {
            var result = await _da.GetListAsync<GetCuisine>(
                 Stored_Procedures.GET_ALL_CUISINE);

            return result;
        }

        public async Task<GetCuisine> Get_Cuisine(int Id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@cuisine_id", Id);
            var result = await _da.GetAsync<GetCuisine>(
                  Stored_Procedures.GET_VEG_NON_VEG, parameters);

            return result;
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

                            int restaId = result.Status;

                            if (restaId <= 0)
                                throw new ApiException("Restaurant already exists", Convert.ToInt32(StatusCode.Conflict));

                            // Insert Files
                            if (item.ResturantImage != null && item.ResturantImage.Any())
                            {
                                foreach (var file in item.ResturantImage)
                                {
                                    var filePath = await FileUploadCommon.UploadFileAsync(file);

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
                            "usp_Update_RestaurantOnProperty",
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
                            "sp_ReplaceRestaurantFiles",
                            deleteParam,
                            transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        // 🔹 3. Upload & Insert new files
                        if (request.ResturantImage != null && request.ResturantImage.Any())
                        {
                            foreach (var file in request.ResturantImage)
                            {
                                var filePath = await FileUploadCommon.UploadFileAsync(file);
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

        public async Task AddPhoneType(AddPhoneType phoneTypeRequest)
        {
            var param = new DynamicParameters();
            param.Add("@phonetype", phoneTypeRequest.PhoneType);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                   Stored_Procedures.ADD_PHONE_TYPE,
                   param
               );
        }

        public async Task<IEnumerable<PhoneTypeResponse>> GetPhoneTypes()
        {

            IEnumerable<PhoneTypeResponse> result = await _da.GetListAsync<PhoneTypeResponse>(
                Stored_Procedures.GET_PHONE_TYPES);

            return result.ToList();
        }

        public async Task<PhoneTypeResponse> GetPhoneType(int phoneTypeId)
        {
            var param = new DynamicParameters();
            param.Add("@phonetype_id", phoneTypeId);

            PhoneTypeResponse result = await _da.GetAsync<PhoneTypeResponse>(
               Stored_Procedures.GET_PHONE_TYPE, param);

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

        public async Task AddAminity(AddAmenitiesRequest aminityRequest)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@AmenityName", aminityRequest.AmenityName, DbType.String);
            parameters.Add("@SortOrder", aminityRequest.SortOrder, DbType.Int32);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                   Stored_Procedures.ADD_AMINITY,
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

        public async Task UpdateAminity(int amenityId, AddAmenitiesRequest aminityRequest)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@AmenityId", amenityId, DbType.Int32);
            parameters.Add("@AmenityName", aminityRequest.AmenityName, DbType.String);
            parameters.Add("@SortOrder", aminityRequest.SortOrder, DbType.Int32);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.UPDATE_AMINITY,
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

        public async Task<IEnumerable<GetAminityResponse>> GetAminities()
        {
            var result = await _da.GetListAsync<GetAminityResponse>(
               Stored_Procedures.GET_AMINITIES);

            return result.ToList();
        }
    }
}