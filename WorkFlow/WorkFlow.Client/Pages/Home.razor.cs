using Syncfusion.Blazor.Diagram;
using System.Text.Json;

namespace WorkFlow.Client.Pages;

public partial class Home
{
    SfDiagramComponent Diagram;
    OrgChartDataModel model;

    private string Role { get;set; }
    private string Team { get; set; }


    public List<OrgChartDataModel> DataSource;

    private MudBlazor.Utilities.MudColor Couleur { get; set; }
    private void OnValidSubmit()
    {

        model = new OrgChartDataModel
        {
            Couleur = Couleur.Value.ToString(),
            Role = Role,
            Team = Team
        };

        if (model != null)
        {
            int count = 1;
            if (DataSource.Count > 0)
            {
                count = DataSource.Count + 1;
            }
            model.Id = count + "";
            DataSource.Add(model);

            Diagram.RefreshDataSource();
        }
        StateHasChanged();
    }

    protected override async Task OnInitializedAsync()
    {
        DataSource = new List<OrgChartDataModel>();
        StateHasChanged();
    }

    //Initializing layout.
    int HorizontalSpacing = 40;
    int VerticalSpacing = 50;

    //To configure every subtree of the organizational chart.
    private TreeInfo GetLayoutInfo(IDiagramObject obj, TreeInfo options)
    {
        options.AlignmentType = SubTreeAlignmentType.Center;
        options.Orientation = Orientation.Vertical;
        return options;
    }

    //Creates node with some default values.
    private void OnNodeCreating(IDiagramObject obj)
    {
        Node node = obj as Node;
        node.Height = 50;
        node.Width = 150;

        if (node.Data is JsonElement)
        {
            node.Data = JsonSerializer.Deserialize<OrgChartDataModel>(node.Data.ToString());
        }
        OrgChartDataModel orgChartDataModel = node.Data as OrgChartDataModel;
        node.Style.Fill = orgChartDataModel.Couleur;
        node.BorderColor = orgChartDataModel.Couleur;

        node.Annotations = new DiagramObjectCollection<ShapeAnnotation>()
        {
            new ShapeAnnotation()
            {
                Content = orgChartDataModel.Role
            }
        };
    }

    //Creates connectors with some default values.
    private void OnConnectorCreating(IDiagramObject connector)
    {
        Connector connectors = connector as Connector;
        connectors.Type = ConnectorSegmentType.Orthogonal;
        connectors.Style = new TextStyle() { StrokeColor = "#6495ED", StrokeWidth = 1 };
        connectors.TargetDecorator = new DecoratorSettings
        {
            Shape = DecoratorShape.None,
            Style = new ShapeStyle() { Fill = "#6495ED", StrokeColor = "#6495ED", }
        };
    }

    public class OrgChartDataModel
    {
        public string Id { get; set; }
        public string Team { get; set; }
        public string Role { get; set; }
        public string Couleur { get; set; }
    }
}
