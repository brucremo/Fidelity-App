using FidelityHub.Application.Interfaces;
using FidelityHub.Application.Models.Sale;
using FidelityHub.Database.Entities.AppSchema;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FidelityHub.Application.Models.Shared;

namespace FidelityHub.Application.Queries.Vendor
{
    public class Query
    {
        #region Queries
        public class AllSales : IRequest<SalesViewModel>
        {
            public int VendorUnitId { get; }
            public DateTime From { get; }
            public DateTime To { get; }

            public AllSales(int vendorUnitId, DateTime from, DateTime to)
            {
                this.VendorUnitId = vendorUnitId;
                this.From = from;
                this.To = to;
            }
        }

        public class SalesChart : IRequest<IEnumerable<GraphModel>>
        {
            public int VendorUnitId { get; }
            public DateTime From { get; }
            public DateTime To { get; }

            public SalesChart(int vendorUnitId, DateTime from, DateTime to)
            {
                this.VendorUnitId = vendorUnitId;
                this.From = from;
                this.To = to;
            }
        }
        #endregion

        #region Handlers
        public class QueryAllSalesHandler : IRequestHandler<AllSales, SalesViewModel>
        {
            public IAppSchemaDataReader Reader { get; }

            public QueryAllSalesHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<SalesViewModel> Handle(AllSales request, CancellationToken cancellationToken)
            {
                return await this.Reader.GetPromotionSaleByVendorUnit(request.VendorUnitId, request.From, request.To);
            }
        }

        public class QuerySalesChartHandler : IRequestHandler<SalesChart, IEnumerable<GraphModel>>
        {
            public IAppSchemaDataReader Reader { get; }

            public QuerySalesChartHandler(IAppSchemaDataReader reader)
            {
                this.Reader = reader;
            }

            public async Task<IEnumerable<GraphModel>> Handle(SalesChart request, CancellationToken cancellationToken)
            {
                var viewModel = await this.Reader.GetPromotionSaleByVendorUnit(request.VendorUnitId, request.From, request.To);

                List<GraphModel> chartData = new List<GraphModel>();

                for (var i = request.From; i < request.To; i = i.AddDays(1))
                {
                    var sales = viewModel.Sales.Select(x => x.Timestamp == i).FirstOrDefault();
                    chartData.Add(new GraphModel
                    {
                        Description = i.ToString(),
                        Data = 0.0
                    });
                }

                return chartData;
            }
        }
        #endregion
    }
}
