using NetArchTest.Rules;
using System.Reflection;

namespace ArchitectureTests
{
    public class CleanArchitectureTest
    {
        private const string DomainNameSpacePath = "Domain";
        private const string ApplicationNameSpacePath = "Application";
        private const string InfrastructureNameSpacePath = "Infrastructure";
        private const string CharityApplicationClientNameSpacePath = "CharityApplication.Client";
        private const string CharityApplicationSharedNameSpacePath = "CharityApplication.Shared";
        private const string CharityApplicationServerNameSpacePath = "CharityApplication.Server";

        [Fact]
        public void DomainLayerTest()
        {
            //Arrange
            var assembly = Assembly.Load(DomainNameSpacePath);

            var references = new[] {
                ApplicationNameSpacePath,
                InfrastructureNameSpacePath,
                CharityApplicationClientNameSpacePath,
                CharityApplicationSharedNameSpacePath,
                CharityApplicationServerNameSpacePath
            };
            //Act
            var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(references).GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }
        [Fact]
        public void ApplicationLayerTest()
        {
            //Arrange
            var assembly = Assembly.Load(ApplicationNameSpacePath);

            var references = new[] {
                InfrastructureNameSpacePath,
                CharityApplicationClientNameSpacePath,
                CharityApplicationServerNameSpacePath
            };
            //Act
            var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(references).GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }
        [Fact]
        public void InfrastructureLayerTest()
        {
            //Arrange
            var assembly = Assembly.Load(InfrastructureNameSpacePath);

            var references = new[] {
                CharityApplicationClientNameSpacePath,
                CharityApplicationServerNameSpacePath
            };
            //Act
            var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(references).GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void CharityApplicationClientLayerTest()
        {
            //Arrange
            var assembly = Assembly.Load(CharityApplicationClientNameSpacePath);

            var references = new[] {
                DomainNameSpacePath,
                ApplicationNameSpacePath,
                InfrastructureNameSpacePath,
                CharityApplicationServerNameSpacePath
            };
            //Act
            var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(references).GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void CharityApplicationSharedLayerTest()
        {
            //Arrange
            var assembly = Assembly.Load(CharityApplicationSharedNameSpacePath);

            var references = new[] {
                DomainNameSpacePath,
                ApplicationNameSpacePath,
                InfrastructureNameSpacePath,
                CharityApplicationClientNameSpacePath,
                CharityApplicationServerNameSpacePath
            };
            //Act
            var result = Types.InAssembly(assembly).ShouldNot().HaveDependencyOnAll(references).GetResult();
            //Assert
            Assert.True(result.IsSuccessful);
        }
    }
}