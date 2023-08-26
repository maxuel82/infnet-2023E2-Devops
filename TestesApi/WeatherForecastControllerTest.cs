using Aula20230826.Controllers;

namespace TestesApi
{
    public class WeatherForecastControllerTest
    {
        [Fact]
        public void DeveFazerGetComSucesso()
        {
            var controller = new WeatherForecastController();

            var result = controller.Get();
            //resolvendo coldesmall01
            Assert.True(result.Any());          
            
        }
    }
}