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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DoTransaction()
        {
            // Create a paymentsRequest
            var amount = new Adyen.Model.Checkout.Amount("AUD", 99);
            var paymentsRequest = new Adyen.Model.Checkout.PaymentRequest
            {
                Reference = "343457546845",
                Amount = amount,
                ReturnUrl = @"ws_489019@Company.SupportRecruitement",
                MerchantAccount = "RecruitementCafe"
            };
            paymentsRequest.AddCardData("4111111111111111", "10", "2020", "737", "John Smith");

            //Create the http client
            var client = new Client(API_KEY, Adyen.Model.Enum.Environment.Test);//or Model.Enum.Environment.Live
            var checkout = new Checkout(client);
            //Make the call to the service. This example code makes a call to /payments
            try
            {
                var paymentsResponse = checkout.Payments(paymentsRequest);
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

        }

        private void Cancel_Click(object sender, EventArgs e)
        {

        }
    }
}
