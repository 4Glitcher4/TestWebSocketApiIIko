using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using TestWebSocketApiIIko.Services;

namespace TestWebSocketApiIIko.Controllers
{
    public class WebsocketController : ControllerBase
    {
        [HttpGet("websocket")]
        public async Task Get()
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                    await Echo(webSocket, "");
                }
                else
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                HttpContext.Response.WriteAsJsonAsync(ex.Message);
            }
        }

        private static async Task Echo(WebSocket webSocket, string data)
        {
            try
            {
                var ser = new ResponseService();

                var buffer = new byte[1024 * 4];
                var receiveResult = await webSocket.ReceiveAsync(
                    new ArraySegment<byte>(buffer), CancellationToken.None);


                while (!receiveResult.CloseStatus.HasValue)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, receiveResult.Count);
                    var requestDataDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(message);

                    byte[] responseData = [];
                    switch (requestDataDict["command"].ToString())
                    {
                        case "Login":
                            responseData = Encoding.UTF8.GetBytes(ser.Login(requestDataDict["data"]));
                            break;
                        case "Sale":
                            responseData = Encoding.UTF8.GetBytes(ser.Sale(requestDataDict["data"]));
                            break;
                        case "Refund":
                            responseData = Encoding.UTF8.GetBytes(ser.Refund(requestDataDict["data"]));
                            break;
                        case "AddPayment":
                            responseData = Encoding.UTF8.GetBytes(ser.AddPayment(requestDataDict["data"]));
                            break;
                        case "ForceReceiptUpload":
                            responseData = Encoding.UTF8.GetBytes(ser.ForceReceiptUpload(requestDataDict["data"]));
                            break;
                        case "CloseZReport":
                            responseData = Encoding.UTF8.GetBytes(ser.ZReport(requestDataDict["data"]));
                            break;
                        case "PrintXReport":
                            responseData = Encoding.UTF8.GetBytes(ser.XReport(requestDataDict["data"]));
                            break;
                        case "PrintLastReceipt":
                            responseData = Encoding.UTF8.GetBytes(ser.PrintLastReceipt(requestDataDict["data"]));
                            break;
                        default:
                            //HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                            break;
                    }

                    await webSocket.SendAsync(new ArraySegment<byte>(responseData, 0, responseData.Length),
                         WebSocketMessageType.Text, receiveResult.EndOfMessage, CancellationToken.None);

                    var outputData = new ArraySegment<byte>(buffer);
                    
                    receiveResult = await webSocket.ReceiveAsync(outputData, CancellationToken.None);
                }

                await webSocket.CloseAsync(
                    receiveResult.CloseStatus.Value,
                    receiveResult.CloseStatusDescription,
                    CancellationToken.None);
            }
            catch (Exception ex)
            {
                var asd = ex.Message;
                throw;
            }
        }


    }
}
