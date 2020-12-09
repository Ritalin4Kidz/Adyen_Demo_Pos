using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adyen;
using Adyen.Service;

namespace AdyenDemoPos
{
    public partial class Form1 : Form
    {
        const string API_KEY = "AQE1hmfxK4LHaxRLw0m/n3Q5qf3Ve55dHZxYTFdTxWq+l3JOk8J4BIF6xrL+9hM035r7qNCLaPMQwV1bDb7kfNy1WIxIIkxgBw==-lyFikbVyg+HZD2y+GgAGF+eB2YDq+VqCh5vLIkfUMS8=-A2paXM3Ta9wWTG6t";
        const string API_ENDPOINT = "https://terminal-api-test.adyen.com/sync";
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
        private void DoQuery()
        {
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
            var POIRequest = new Adyen.Model.Nexo.SaleToPOIMessage
            {
                MessageHeader = new Adyen.Model.Nexo.MessageHeader
                {
                    MessageClass = Adyen.Model.Nexo.MessageClassType.Service,
                    MessageCategory = Adyen.Model.Nexo.MessageCategoryType.CardAcquisition,
                    MessageType = Adyen.Model.Nexo.MessageType.Request,
                    ServiceID = "1020711110",
                    SaleID = "POSSystemID12345",
                    POIID = "V400m-346190779"
                },
                MessagePayload = queryRequest
            };
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);
            var checkout = new Adyen.Service.PosPaymentCloudApi(client);
            try
            {
                var QueryResponse = checkout.TerminalApiCloudSync(POIRequest);
                Receipt.Text = QueryResponse.MessagePayload.ToString();
            }
            catch (Exception e)
            {
                Receipt.Text = e.StackTrace;
            }
        }

        private void DoCancel()
        {
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
            var POIRequest = new Adyen.Model.Nexo.SaleToPOIMessage
            {
                MessageHeader = new Adyen.Model.Nexo.MessageHeader
                {
                    MessageClass = Adyen.Model.Nexo.MessageClassType.Service,
                    MessageCategory = Adyen.Model.Nexo.MessageCategoryType.CardAcquisition,
                    MessageType = Adyen.Model.Nexo.MessageType.Request,
                    ServiceID = "1020711110",
                    SaleID = "POSSystemID12345",
                    POIID = "V400m-346190779"
                },
                MessagePayload = abortRequest
            };
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);
            var checkout = new Adyen.Service.PosPaymentCloudApi(client);
            try
            {
                var AbortResponse = checkout.TerminalApiCloudSync(POIRequest);
                Receipt.Text = AbortResponse.MessagePayload.ToString();
            }
            catch (Exception e)
            {
                Receipt.Text = e.StackTrace;
            }

        }

        private void DoTransaction()
        {
            // Create a paymentsRequest
            var paymentsRequest = new Adyen.Model.Nexo.PaymentRequest
            {
                SaleData = new Adyen.Model.Nexo.SaleData
                {
                    SaleTransactionID = new Adyen.Model.Nexo.TransactionIdentification
                    {
                        TransactionID = "02072",
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
                }
            };
            var POIRequest = new Adyen.Model.Nexo.SaleToPOIMessage
            {
                MessageHeader = new Adyen.Model.Nexo.MessageHeader
                {
                    MessageClass = Adyen.Model.Nexo.MessageClassType.Service,
                    MessageCategory = Adyen.Model.Nexo.MessageCategoryType.CardAcquisition,
                    MessageType = Adyen.Model.Nexo.MessageType.Request,
                    ServiceID = "1020711110",
                    SaleID = "POSSystemID12345",
                    POIID = "V400m-346190779"
                },
                MessagePayload = paymentsRequest
            };
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);
            var checkout = new Adyen.Service.PosPaymentCloudApi(client);
            try
            {
                var PaymentResponse = checkout.TerminalApiCloudSync(POIRequest);
                Receipt.Text = PaymentResponse.MessagePayload.ToString();
            }
            catch (Exception e)
            {
                Receipt.Text = e.StackTrace;
            }
        }

        private void Tender_Click(object sender, EventArgs e)
        {
            DoTransaction();
        }

        private void Query_Click(object sender, EventArgs e)
        {
            DoQuery();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DoCancel();
        }
    }
}
