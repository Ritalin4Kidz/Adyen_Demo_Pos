using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adyen;
using Adyen.Model.Nexo;
using Adyen.Service;
using Newtonsoft.Json.Linq;

namespace AdyenDemoPos
{
    public partial class Form1 : Form
    {
        const string API_KEY = "AQE1hmfxK4LHaxRLw0m/n3Q5qf3Ve55dHZxYTFdTxWq+l3JOk8J4BIF6xrL+9hM035r7qNCLaPMQwV1bDb7kfNy1WIxIIkxgBw==-lyFikbVyg+HZD2y+GgAGF+eB2YDq+VqCh5vLIkfUMS8=-A2paXM3Ta9wWTG6t";
        const string API_ENDPOINT = "https://terminal-api-test.adyen.com/sync";
        string CURRENT_SERVICE_ID = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        /*
          "CardAcquisitionRequest":{
            "SaleData":{
                "SaleTransactionID":{
                    "TransactionID":"02072",
                    "TimeStamp":"2020-01-07T14:14:04+00:00"
                },
                "TokenRequestedType":"Customer"
            },
            "CardAcquisitionTransaction":{
                "TotalAmount":24.98
            }
        }
         *
         */
        private async Task DoQueryAsync()
        {
           Adyen.Model.Nexo.MessageHeader POIMessageHeader = new Adyen.Model.Nexo.MessageHeader()
           {
               MessageClass = Adyen.Model.Nexo.MessageClassType.Service,
               MessageCategory = Adyen.Model.Nexo.MessageCategoryType.CardAcquisition,
               MessageType = Adyen.Model.Nexo.MessageType.Request,
               ServiceID = DateTime.Now.ToString("ddHHmmss"),
               SaleID = "POSSystemID12345",
               POIID = "V400m-346190779"
           };
            var queryRequest = new Adyen.Model.Nexo.CardAcquisitionRequest
            {
                SaleData = new Adyen.Model.Nexo.SaleData
                {
                    SaleTransactionID = new Adyen.Model.Nexo.TransactionIdentification
                    {
                        TransactionID = "02072",
                        TimeStamp = DateTime.Now
                    },
                    TokenRequestedType = Adyen.Model.Nexo.TokenRequestedType.Customer
                },
                CardAcquisitionTransaction = new Adyen.Model.Nexo.CardAcquisitionTransaction
                {
                    TotalAmount = 24.98M
                }
            };
            //var amount = new Adyen.Model.Checkout.Amount("AUD", 99);
            var POIRequest = new Adyen.Model.Nexo.Message.SaleToPOIRequest
            {
                MessageHeader = POIMessageHeader,
                MessagePayload = queryRequest,              
                SecurityTrailer = new ContentInformation() { }
            };
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);
            var checkout = new Adyen.Service.PosPaymentCloudApi(client);
            try
            {
                var _QueryResponse = checkout.TerminalApiCloudSync(POIRequest);
                Adyen.Model.Nexo.CardAcquisitionResponse r = (Adyen.Model.Nexo.CardAcquisitionResponse)_QueryResponse.MessagePayload;
                Receipt.Text = $"Success: {r.Response.Result}\n";
            }
            catch (Exception e)
            {
                Receipt.Text = e.StackTrace;
            }
        }

        private async Task DoCancelAsync()
        {
            Adyen.Model.Nexo.MessageHeader POIMessageHeader = new Adyen.Model.Nexo.MessageHeader()
            {
                MessageClass = Adyen.Model.Nexo.MessageClassType.Service,
                MessageCategory = Adyen.Model.Nexo.MessageCategoryType.Abort,
                MessageType = Adyen.Model.Nexo.MessageType.Request,
                ServiceID = CURRENT_SERVICE_ID,
                SaleID = "POSSystemID12345",
                POIID = "V400m-346190779"
            };
            //var amount = new Adyen.Model.Checkout.Amount("AUD", 99);
            var abortRequest = new Adyen.Model.Nexo.AbortRequest
            {
                AbortReason = "MerchantAbort",
                MessageReference = new Adyen.Model.Nexo.MessageReference
                {
                    MessageCategory = Adyen.Model.Nexo.MessageCategoryType.Payment,
                    SaleID = "POSSystemID12345",
                    ServiceID = "21796"
                }
            };
            //var amount = new Adyen.Model.Checkout.Amount("AUD", 99);
            var POIRequest = new Adyen.Model.Nexo.Message.SaleToPOIRequest
            {
                MessageHeader = POIMessageHeader,
                MessagePayload = abortRequest,
                SecurityTrailer = new ContentInformation() { }
            };
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);
            var checkout = new Adyen.Service.PosPaymentCloudApi(client);
            try
            {
                var _AbortResponse = checkout.TerminalApiCloudSync(POIRequest);
                Adyen.Model.Nexo.AbortRequest r = (Adyen.Model.Nexo.AbortRequest)_AbortResponse.MessagePayload;
                Receipt.Text = $"Transaction Aborted: {r.AbortReason}";
            }
            catch (Exception e)
            {
                Receipt.Text = e.StackTrace;
            }

        }

        private async Task DoTransactionAsync()
        {
            CURRENT_SERVICE_ID = DateTime.Now.ToString("ddHHmmss");
            Adyen.Model.Nexo.MessageHeader POIMessageHeader = new Adyen.Model.Nexo.MessageHeader()
            {
                MessageClass = Adyen.Model.Nexo.MessageClassType.Service,
                MessageCategory = Adyen.Model.Nexo.MessageCategoryType.Payment,
                MessageType = Adyen.Model.Nexo.MessageType.Request,
                ServiceID = CURRENT_SERVICE_ID,
                SaleID = "RecruitementCafe",
                POIID = "V400m-346190779"
            };
            var paymentRequest = new Adyen.Model.Nexo.PaymentRequest
            {
                SaleData = new Adyen.Model.Nexo.SaleData
                {
                    SaleTransactionID = new Adyen.Model.Nexo.TransactionIdentification
                    {
                        TransactionID = DateTime.Now.ToString("ddHHmmss"),
                        TimeStamp = DateTime.Now
                    },
                },
                PaymentTransaction = new Adyen.Model.Nexo.PaymentTransaction
                {
                    AmountsReq = new Adyen.Model.Nexo.AmountsReq
                    {
                        Currency = "AUD",
                        RequestedAmount = 25.00M
                    }
                },
                PaymentData = new Adyen.Model.Nexo.PaymentData
                {
                    PaymentType = Adyen.Model.Nexo.PaymentType.Normal
                }
            };
            var POIRequest = new Adyen.Model.Nexo.Message.SaleToPOIRequest
            {              
                MessageHeader = POIMessageHeader,
                MessagePayload = paymentRequest,
                SecurityTrailer = new ContentInformation() { }
            };
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);
            var checkout = new Adyen.Service.PosPaymentCloudApi(client);
            try
            {
                var _PaymentResponse = checkout.TerminalApiCloudSync(POIRequest);
                Adyen.Model.Nexo.PaymentResponse r = (Adyen.Model.Nexo.PaymentResponse)_PaymentResponse.MessagePayload;
                Receipt.Text = $"Success: {r.Response.Result}\nAmount:{r.PaymentResult.AmountsResp.AuthorizedAmount} \n";
            }
            catch (Exception e)
            {
                Receipt.Text = e.StackTrace;
            }
        }

        private async void Tender_ClickAsync(object sender, EventArgs e)
        {
            await DoTransactionAsync();
        }

        private async void Query_ClickAsync(object sender, EventArgs e)
        {
            await DoQueryAsync();
        }

        private async void Cancel_ClickAsync(object sender, EventArgs e)
        {
            await DoCancelAsync();
        }
    }
}
