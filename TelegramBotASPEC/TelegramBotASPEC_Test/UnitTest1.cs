namespace TelegramBotASPEC_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddReqIsSucssesful()
        {
            //arrange
            string text = "text";
            long chatId = 1; 
            int messageId = 1;

            //act
            TelegramBotASPEC.ReqManager.AddReq(text, chatId, messageId);

            // assert
            Assert.AreEqual(1, TelegramBotASPEC.ReqManager.reqs.Count);
        }

        [Test]
        public void FindReqIsSucssesful()
        {
            //arrange
            string text = "text";
            long chatId = 1;
            int messageId = 1;

            //act
            TelegramBotASPEC.ReqManager.AddReq(text, chatId, messageId);
            var req = TelegramBotASPEC.ReqManager.reqs[0];
            var newreq = TelegramBotASPEC.ReqManager.FindReqByID(messageId);

            // assert
            Assert.AreEqual(req, newreq);
        }

        [Test]
        public void FindReqIsSucssesful2()
        {
            //arrange
            string text = "text";
            long chatId = 1;
            int messageId = 1;

            //act
            TelegramBotASPEC.ReqManager.AddReq(text, chatId, messageId);
            var req = TelegramBotASPEC.ReqManager.reqs[0];
            var newreq = TelegramBotASPEC.ReqManager.FindReqByText(text);

            // assert
            Assert.AreEqual(req, newreq);
        }

        [Test]
        public void AddBotUpdatesIsSucssesful()
        {
            //arrange
            string text = "text";
            long chatId = 1;


            //act
            TelegramBotASPEC.BotUpdateManager.AddBotUpdates(text, chatId);

            // assert
            Assert.AreEqual(1, TelegramBotASPEC.BotUpdateManager.botUpdates.Count);
        }

        [Test]
        public void AddBotUpdatesIsSucssesful2()
        {
            //arrange
            string text = "text";
            long chatId = 1;
            var newBotUpdate = new TelegramBotASPEC.BotUpdate(text, chatId);

            //act
            TelegramBotASPEC.BotUpdateManager.AddBotUpdates(newBotUpdate);

            // assert
            Assert.AreEqual(2, TelegramBotASPEC.BotUpdateManager.botUpdates.Count);
        }
    }
}