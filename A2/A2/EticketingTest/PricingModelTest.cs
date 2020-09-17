using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSE598_A2_E_Ticketing;


namespace EticketingTest
{
    [TestClass]
    public class PricingModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PricingModel pm = new PricingModel();
            for(int i = 0; i < 200; i++)
            {
                int expectedPrevPrice = pm.getCurrentPrice();
                int price = pm.updatePrice();
                Assert.IsTrue(price >= 80 && price <= 300);
                Assert.AreEqual(expectedPrevPrice, pm.getPreviousPrice());
            }
        }
    }
}
