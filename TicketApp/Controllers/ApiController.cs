using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TicketApp.Models.ApiResponse;
using TicketApp.Models.Journey;
using TicketApp.Models.Message;
using static TicketApp.Models.ApiResponse.BusLocationApiResponse;

namespace TicketApp.Controllers
{
    public class ApiController : Controller
    {
        private HttpClient httpClient;
        private Uri url = new Uri("https://v2-api.obilet.com/api/");
        private string apiClientToken = "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
        private string contentType = "application/json";

        public ApiController()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", apiClientToken);
        }

        [HttpPost]
        public async Task<SessionApiResponse> GetSession()
        {
            try
            {
                StringContent request = new StringContent("{\"type\":1,\"connection\":{\"ip-address\":\"145.214.41.2\", \"port\":\"5117\"},\"browser\":{\"name\":\"Chrome\", \"version\":\"47.0.0.12\"}}", Encoding.UTF8, contentType);
                HttpResponseMessage response = await httpClient.PostAsync(url + "client/getsession", request);
                var result = response.Content.ReadAsStringAsync().Result;
                SessionApiResponse sessionApiResponse = JsonConvert.DeserializeObject<SessionApiResponse>(result);

                return sessionApiResponse;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occured. Error: " + ex.Message, ex);
            }
        }

        [HttpPost]
        public async Task<List<JourneyDetail>> GetBusLocations(GeneralMessage message)
        {
            try
            {
                StringContent request = new StringContent("{\"mode\": \"raw\",\"raw\": \"{\n    \"data\":null,\n \"device-session\":{\n \"session-id\":" + message.SessionId + ",\n \"device-id\": " + message.DeviceId + "\n  },\n  \"date\":" + message.Date + ",\n \"language\":\"tr-TR\"\n}\"}", Encoding.UTF8, contentType);
                HttpResponseMessage response = await httpClient.PostAsync(url + "/journey/getbusjourneys", request);
                var result = response.Content.ReadAsStringAsync().Result;
                JourneyApiResponse busLocationApiResponse = JsonConvert.DeserializeObject<JourneyApiResponse>(result);

                var sampleResult = GetSampleJourney();
                return ConvertApiToJourney(sampleResult);


            }
            catch (Exception ex)
            {
                var result = GetSampleJourney();
                return ConvertApiToJourney(result);

            }
       }

        [HttpPost]
        public async Task<JourneyApiResponse> GetJourneys(GeneralMessage message, int originID, int destinationId)
        {
            try
            {
                StringContent request = new StringContent("{\"mode\": \"raw\",\"raw\": \"{\n  \"device-session\": {\n \"session-id\": " + message.SessionId + ",\n \"device-id\": " + message.DeviceId + "\n  },\n \"date\": " + message.Date + ",\n  \"language\": \"tr-TR\",\n \"data\": {\n    \"origin-id\": " + originID + ",\n    \"destination-id\": " + destinationId + ",\n  \"departure-date\": " + message.Date + "\n  }\n}\"}", Encoding.UTF8, contentType);
                HttpResponseMessage response = await httpClient.PostAsync(url + "/journey/getbusjourneys", request);
                var result = response.Content.ReadAsStringAsync().Result;
                JourneyApiResponse journeyApiResponse = JsonConvert.DeserializeObject<JourneyApiResponse>(result);

                return journeyApiResponse;
            }
            catch (Exception ex)
            {
                //throw new Exception("An error occured. Error: "+ ex.Message, ex);
                return GetSampleJourney();
            }

        }

        private JourneyApiResponse GetSampleJourney()
        {
            JourneyApiResponse journeyApiResponse = new JourneyApiResponse()
            {
                Data = new List<JourneyApiResponseData>()
                {
                    new JourneyApiResponseData()
                    {
                        Id = 73083886,
                        PartnerId = 330,
                        PartnerName = "Metro Turizm",
                        RouteId = 59275,
                        BusType = "2+1",
                        TotalSeats = 41,
                        AvailableSeats = 16,
                        Journey = new JourneyDetailApiResponse()
                        {
                            Kind = "Bus",
                            Code = "9064-ALIBEYKOY TESIS-ANKARA-20190112",
                            Stops = new List<JourneyStopsApiResponse>()
                            {
                                new JourneyStopsApiResponse()
                                {
                                    Name = "Keşan Otogarı",
                                    Station = "255",
                                    Time = "2021-02-06T19:30:00",
                                    IsOrigin = false,
                                    IsDestination = false
                                }
                            },
                            Origin = "Alibeyköy Otogarı",
                            Destination = "Ankara (Aşti) Otogarı",
                            Departure = "2021-02-07T00:00:00",
                            Arrival = "2021-02-07T07:00:00",
                            Currency = "TRY",
                            Duration = "07:00:00",
                            OriginalPrice =100,
                            InternetPrice ="70",
                            Booking = null,
                            BusName = "SUIT",
                            Policy = new JourneyPolicyApiResponse()
                            {
                                MaxSeats = null,
                                MaxSingle = null,
                                MaxSingleFemales = null,
                                MaxSingleMales = null,
                                MixedGenders = false,
                                GovId = true,
                                Lht = false
                            },
                            Features = new List<string>()
                            {
                                "Kablosuz Internet (WiFi)",
                                "Koltuk ekraninda TV yayını",
                                "Enflasyonla Topyekûn Mücadele İçin %10 İndirimli"
                            },
                            Description = null,
                            Available = null
                        },
                        Features = new List<JourneyApiResponseFeatures>()
                        {
                            new JourneyApiResponseFeatures()
                            {
                                Id = 26,
                                Priority = 1,
                                Name = "Enflasyonla Topyekûn Mücadele İçin %10 İndirimli",
                                Description = null,
                                IsPromoted= false,
                                BackColor= null,
                                ForeColor= null
                            }
                        },
                        OriginLocation = "İstanbul Avrupa",
                        DestinationLocation= "Ankara",
                        IsActive = true,
                        OriginLocationId = 349,
                        DestinationLocationId = 356,
                        IsPromoted = false,
                        CancellationOffset = 9,
                        HasBusShuttle = true,
                        DisableSalesWithoutGovId = false,
                        DisplayOffset = null,
                        PartnerRating = 7.8m
                    }
                },
                Message = null,
                UserMessage = null,
                ApiRequestId = null,
                Controller = "Journey"
            };



            return journeyApiResponse;
        }

        private List<JourneyDetail> ConvertApiToJourney(JourneyApiResponse journeyApiResponse)
        {
            List<JourneyDetail> result = new List<JourneyDetail>();
            foreach (var item in journeyApiResponse.Data)
            {
                JourneyDetail journeyDetail = new JourneyDetail()
                {
                    OriginId = item.OriginLocationId,
                    DestinationId = item.DestinationLocationId,
                    StartHour = item.Journey.Departure.Substring(item.Journey.Departure.IndexOf("T") + 1, 5),
                    EndHour = item.Journey.Arrival.Substring(item.Journey.Arrival.IndexOf("T") + 1, 5),
                    Price = item.Journey.InternetPrice.ToString().Contains(".")==false ? item.Journey.InternetPrice.ToString() + ".00 " + item.Journey.Currency : item.Journey.InternetPrice.ToString() + " "+ item.Journey.Currency,
                    StartPoint = item.Journey.Origin,
                    EndPoint = item.Journey.Destination
                };
                result.Add(journeyDetail);
            }
            return result;
        }

        private BusLocationApiResponse GetSampleBusLocation()
        {
            GeoLocation geoLocation = new GeoLocation()
            {
                Latitude = 41.0620024,
                Longitude = 28.8913871,
                Zoom = "12"
            };
            return new BusLocationApiResponse()
            {
                Status = "Success",
                ApiRequestId = null,
                ClientRequestId = null,
                Controller = "Location",
                Message = null,
                UserMessage = null,
                Data = new List<BusLocationResponseData>() {
                    new BusLocationResponseData()
                    {
                        Id = 349,
                        ParentId = 250,
                        Type = "Town",
                        Name = "İstanbul Avrupa",
                        GeoLocation = geoLocation,
                        TzCode = "Turkey Standart Time",
                        WeatherCode= null,
                        Rank = 1,
                        ReferenceCode = null,
                        Keywords = "Otobüs Kalkış-Varış Noktası Kıraç Mecidiyekoy"

                    }
                }

            };
        }
    }
}
