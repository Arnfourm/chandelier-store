using microservices.CatalogAPI.API.Contracts.Requests;
using microservices.CatalogAPI.API.Contracts.Responses;
using microservices.CatalogAPI.Domain.Interfaces.DAO;
using microservices.CatalogAPI.Domain.Interfaces.Services;
using microservices.CatalogAPI.Domain.Models;
using Attribute = microservices.CatalogAPI.Domain.Models.Attribute;

namespace microservices.CatalogAPI.Domain.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly IAttributeDAO _attributeDAO;

        private readonly IAttributeGroupService _attributeGroupService;
        private readonly IMeasurementUnitService _measurementUnitService;

        public AttributeService(
            IAttributeDAO attributeDAO, 
            IAttributeGroupService attributeGroupService, 
            IMeasurementUnitService measurementUnitService
        )
        {
            _attributeDAO = attributeDAO;

            _attributeGroupService = attributeGroupService;
            _measurementUnitService = measurementUnitService;
        }
        
        public async Task<IEnumerable<AttributeResponse>> GetAllAttributes()
        {
            List<Attribute> attributes = await _attributeDAO.GetAttributes();

            List<int> attributeGroupIds = attributes.Select(attribute => attribute.GetAttributeGroupId()).ToList();
            List<int> measurementUnitIds = attributes.Select(attribute => attribute.GetMeasurementUnitId()).ToList();

            IEnumerable<AttributeGroup> attributeGroups = await _attributeGroupService.GetListAttributeGroupByIds(attributeGroupIds);
            IEnumerable<MeasurementUnit> measurementUnits = await _measurementUnitService.GetListMeasurementUnitByIds(measurementUnitIds);

            var attributeGroupDict = attributeGroups.ToDictionary(attributeGroup => attributeGroup.GetId());
            var measurementUnitDict = measurementUnits.ToDictionary(measurementUnit => measurementUnit.GetId());

            IEnumerable<AttributeResponse> response = attributes.Select(attribute =>
            {
                AttributeGroup attributeGroup = attributeGroupDict[attribute.GetAttributeGroupId()];
                MeasurementUnit measurementUnit = measurementUnitDict[attribute.GetMeasurementUnitId()];

                return new AttributeResponse(
                    attribute.GetId(),
                    attribute.GetTitle(),
                    attributeGroup.GetTitle(),
                    measurementUnit.GetTitle()
                );
            });

            return response;
        }

        public async Task<Attribute> GetSingleAttributeById(Guid id)
        {
            Attribute attribute = await _attributeDAO.GetAttributeById(id);

            return attribute;
        }

        public async Task CreateNewAttribute(AttributeRequest request)
        {
            AttributeGroup attributeGroup = await _attributeGroupService.GetSingleAttributeGroupByTitle(request.AttributeGroup);
            MeasurementUnit measurementUnit = await _measurementUnitService.GetSingleMeasurementUnitByTitle(request.MeasurementUnit);

            Attribute newAttribute = new Attribute(
                request.Title,
                attributeGroup.GetId(),
                measurementUnit.GetId()
            );
            await _attributeDAO.CreateAttribute(newAttribute);
        }

        public async Task UpdateAttribute(Guid id, AttributeRequest request)
        {
            AttributeGroup attributeGroup = await _attributeGroupService.GetSingleAttributeGroupByTitle(request.AttributeGroup);
            MeasurementUnit measurementUnit = await _measurementUnitService.GetSingleMeasurementUnitByTitle(request.MeasurementUnit);

            Attribute updateAttribute = new Attribute(
                id,
                request.Title,
                attributeGroup.GetId(),
                measurementUnit.GetId()
            );

            await _attributeDAO.UpdateAttribute(updateAttribute);
        }

        public async Task DeleteSingleAttributeById(Guid id)
        {
            await _attributeDAO.DeleteAttributeById(id);
        }
    }
}