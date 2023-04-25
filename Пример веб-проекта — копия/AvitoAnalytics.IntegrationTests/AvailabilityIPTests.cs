namespace FirstWebApplication.IntegrationTests
{
    public class Tests
    {
        [Test]
        public async Task CheckAvailabilityOfIp()
        {
            var ads = new Ad[]
            {
                new Ad()
                {
                    AdID = "2652407788",
                    Link = @"https://www.avito.ru/moskva/predlozheniya_uslug/kursovye_diplomnye_raboty_2652407788",
                    KeyWords = "курсовая работа"
                },

                new Ad()
                {
                    AdID = "2556371385",
                    Link = @"https://www.avito.ru/ekaterinburg/predlozheniya_uslug/kursovaya_diplomnaya_rabota_2556371385",
                    KeyWords = "курсовая работа"
                }
            };

            var avitoInformationProvider = new AvitoInformationProvider(new AvitoClient());
            foreach (var ad in ads)
            {
                var position = await avitoInformationProvider.GetPositionAsync(ad);
                Assert.That(position, Is.Not.EqualTo(0));
            }
        }
    }
}