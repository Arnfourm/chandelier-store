using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;
using System.Linq;
using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Services
{
    public class ProductAttributeService : IProductAttributeService
    {
        private readonly IProductAttributeDAO _productAttributeDAO;

        private readonly IAttributeService _attributeService;
        private readonly IAttributeGroupService _attributeGroupService;
        private readonly IMeasurementUnitService _measurementUnitService;
        private readonly IProductService _productService;

        public ProductAttributeService(
            IProductAttributeDAO productAttributeDAO,
            IAttributeService attributeService,
            IAttributeGroupService attributeGroupService,
            IMeasurementUnitService measurementUnitService,
            IProductService productService
        )
        {
            _productAttributeDAO = productAttributeDAO;

            _attributeService = attributeService;
            _attributeGroupService = attributeGroupService;
            _measurementUnitService = measurementUnitService;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductAttributeResponse>> GetProductAttributeByProductId(Guid productId)
        {
            List<ProductAttribute> productAttributes = await _productAttributeDAO.GetProductAttributeByProductId(productId);

            List<Guid> attributeIds = productAttributes.Select(productAttribute => productAttribute.GetAttributeId()).ToList();

            IEnumerable<AttributeResponse> attributes = await _attributeService.GetListAttributeResponseByIds(attributeIds);

            var attributeDict = attributes.ToDictionary(attribute => attribute.Id);

            IEnumerable<ProductAttributeResponse> response = productAttributes.Select(productAttribute =>
            {
                AttributeResponse attributeResponse = attributeDict[productAttribute.GetAttributeId()];

                return new ProductAttributeResponse(
                    attributeResponse,
                    productAttribute.GetValue()
                );
            });

            return response;
        }

        public async Task CreateNewSingleProductAttribute(ProductAttributeRequest request)
        {
            Product product = await _productService.GetSingleProductById(request.ProductId);
            Attribute attribute = await _attributeService.GetSingleAttributeById(request.AttributeId);

            ProductAttribute productAttribute = new ProductAttribute(
                request.ProductId, 
                request.AttributeId,
                request.Value
            );

            await _productAttributeDAO.CreateProductAttribute(productAttribute);
        }

        public async Task UpdateSingleProductAttribute(ProductAttributeRequest request)
        {
            Product product = await _productService.GetSingleProductById(request.ProductId);
            Attribute attribute = await _attributeService.GetSingleAttributeById(request.AttributeId);

            ProductAttribute updateProductAttribute = new ProductAttribute(
                request.ProductId,
                request.AttributeId,
                request.Value
            );

            await _productAttributeDAO.UpdateProductAttribute(updateProductAttribute);
        }
    }
}