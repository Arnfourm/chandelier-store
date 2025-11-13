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

            IEnumerable<Attribute> attributes = await _attributeService.GetListAttributeByIds(attributeIds);


            List<int> attributeGroupIds = attributes.Select(attribute => attribute.GetAttributeGroupId()).ToList();
            List<int> measurementUnitIds = attributes.Select(attribute => attribute.GetMeasurementUnitId()).ToList();

            IEnumerable<AttributeGroup> attributeGroups = await _attributeGroupService.GetListAttributeGroupByIds(attributeGroupIds);
            IEnumerable<MeasurementUnit> measurementUnits = await _measurementUnitService.GetListMeasurementUnitByIds(measurementUnitIds);

            var attributeDict = attributes.ToDictionary(attribute => attribute.GetId());
            var attributeGroupDict = attributeGroups.ToDictionary(attributeGroup => attributeGroup.GetId());
            var measurementUnitDict = measurementUnits.ToDictionary(measurementUnit => measurementUnit.GetId());

            IEnumerable<ProductAttributeResponse> response = productAttributes.Select(productAttribute =>
            {
                Attribute attribute = attributeDict[productAttribute.GetAttributeId()];
                AttributeGroup attributeGroup = attributeGroupDict[attribute.GetAttributeGroupId()];
                MeasurementUnit measurementUnit = measurementUnitDict[attribute.GetMeasurementUnitId()];

                return new ProductAttributeResponse(
                    new AttributeResponse(
                        attribute.GetId(),
                        attribute.GetTitle(),
                        new AttributeGroupResponse(attributeGroup.GetId(), attributeGroup.GetTitle()),
                        new MeasurementUnitResponse(measurementUnit.GetId(), measurementUnit.GetTitle())
                    ),
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