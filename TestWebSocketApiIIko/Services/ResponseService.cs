using Newtonsoft.Json;

namespace TestWebSocketApiIIko.Services
{
    public class ResponseService
    {
        public string Login(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["command"] = "Login";
            response["status"] = payload.ContainsKey("login") && payload["login"].ToString() == "admin" ? "Success" : "Incorrect login";
            return JsonConvert.SerializeObject(response);
        }

        public string Sale(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["id"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "Sale";

            response["payload"] = new Dictionary<string, object>
        {
            {"type", "Sale"},
            {"saleId", "019207210000"}
        };

            if (payload.ContainsKey("payments"))
            {
                var payments = new List<Dictionary<string, object>>();

                var asd = ((payload["payments"] as dynamic)[0] as dynamic)["amount"];
                var payment = new Dictionary<string, object>
            {
                {"amount", ((payload["payments"] as dynamic)[0] as dynamic)["amount"]},
                {"paymentType",( (payload["payments"]as dynamic)[0] as dynamic)["paymentType"]},
                {"cardPaymentParams", new Dictionary<string, object>
                    {
                        {"responseCode", "65"},
                        {"merchantId", "00000009"},
                        {"terminalId", "000000009002119"},
                        {"authCode", "005746"},
                        {"stan", "53"},
                        {"cardType", "MASTER International"},
                        {"rrn", "000117912425"},
                        {"transactionNumber", "26"}
                    }
                }
            };
                payments.Add(payment);
                (response["payload"] as dynamic)["payments"] = payments;
            }

          (response["payload"] as dynamic)["saleReceipt"] = new Dictionary<string, object>
        {
            {"mName", "Dev Team"},
            {"mAddress", "Тестовый адрес 1"},
            {"tin", "303109711"},
            {"date", "2022-07-21"},
            {"time", "12:19:09"},
            {"saleId", "019207210000"},
            {"zNumber", "56"},
            {"uName", "Админ Админов"},
            {"operation", "Продажа"},
            {"productList", new List<Dictionary<string, object>>
                {
                    new Dictionary<string, object>
                    {
                        {"name", "тестовый продукт"},
                        {"amount", "1.0"},
                        {"price", "5 000.00"},
                        {"totalPrice", "5 000.00"},
                        {"vat", "454.55"},
                        {"vatPercent", "0.1"},
                        {"mxik", "12341234123412341234123"},
                        {"commissionTIN", "123123123"},
                        {"barcode", "4674647484949"},
                        {"label", "01467464748494921yrtril91"}
                    }
                }
            },
            {"qr", "https://ofd.soliq.uz/check?t=UZ190912010634&r=1679&c=20220721121909&s=499408544365"},
            {"cashback", "Вы получили право на “кешбек” в размере 1% от покупки!"}
        };

            if (payload.ContainsKey("payments"))
            {
                (response["payload"] as dynamic)["skPayReceipt"] = new Dictionary<string, object>
            {
                {"receiptType", "MPS"},
                {"merchantName", "UZ"},
                {"merchantAddress", "UZ"},
                {"cLess", "БЕСКОНТАКТНАЯ ОПЕРАЦИЯ"},
                {"transactionNumber", "26"},
                {"tId", "00000009"},
                {"mId", "000000009002119"},
                {"softName", "Uzcard POS v1.2.0"},
                {"sn", "1170393019"},
                {"rrn", "000117912425"},
                {"date", "2022-07-21 12:18:59"},
                {"cardMask", "************5124"},
                {"aid", "A0000000041010"},
                {"authCode", "005746"},
                {"cardType", "MASTERCARD"},
                {"tvr", "0000008001"},
                {"tsi", "0000"},
                {"aip", "1980"},
                {"transactionType", "Оплата"},
                {"transactionAmount", "5 000.00"},
                {"transactionStatus", "Одобрено"},
                {"commissionAmount", "0.00"},
                {"total", "5 000.00"},
                {"authMethod", "Подпись"},
                {"responseCode", "000"},
                {"currency", "UZS"},
                {"applicationName", "MASTERCARD"}
            };
            }

            return JsonConvert.SerializeObject(response);
        }

        public string Refund(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "Refund";

            response["payload"] = new Dictionary<string, object>
        {
            {"type", "Refund"},
            {"saleId", "019207210000"}
        };

            if (payload.ContainsKey("payments"))
            {
                (response["payload"] as dynamic)["payments"] = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    {"amount", "500000"},
                    {"paymentType", "Cash"}
                }
            };
            }

            (response["payload"] as dynamic)["refundReceipt"] = null;
            (response["payload"] as dynamic)["skPayReceipt"] = null;
            return JsonConvert.SerializeObject(response);
        }

        public string AddPayment(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "AddPayment";

            response["payload"] = new Dictionary<string, object>();

            if (payload.ContainsKey("payment"))
            {
                (response["payload"] as dynamic)["payment"] = new Dictionary<string, object>
            {
                {"amount", "500000"},
                {"paymentType", "Cash"}
            };
            }

            return JsonConvert.SerializeObject(response);
        }

        public string XReport(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "PrintXReport";

            response["payload"] = null;
            return JsonConvert.SerializeObject(response);
        }

        public string ZReport(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "CloseZReport";

            response["payload"] = null;
            return JsonConvert.SerializeObject(response);
        }

        public string ForceReceiptUpload(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "ForceReceiptUpload";

            response["payload"] = null;
            return JsonConvert.SerializeObject(response);
        }

        public string PrintLastReceipt(dynamic payload)
        {
            var response = new Dictionary<string, object>();
            response["requestId"] = Guid.NewGuid().ToString();
            response["status"] = "Success";
            response["error"] = "PaperEnded";
            response["command"] = "PrintLastReceipt";

            response["payload"] = null;
            return JsonConvert.SerializeObject(response);
        }
    }
}
