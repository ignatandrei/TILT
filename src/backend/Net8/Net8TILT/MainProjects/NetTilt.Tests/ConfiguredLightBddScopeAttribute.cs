using LightBDD.Core.Results.Parameters.Tabular;

namespace NetTilt.Tests;

internal class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
{
    protected override void OnConfigure(LightBddConfiguration configuration)
    {
        configuration
            .ReportWritersConfiguration()
            .Clear()
            .AddFileWriter<XmlReportFormatter>(@"~/Reports/FeaturesReport.xml")
            //.AddFileWriter<PlainTextReportFormatter>("~/Reports/{TestDateTimeUtc:yyyy-MM-dd-HH_mm_ss}_FeaturesReport.txt")
            .AddFileWriter<PlainTextReportFormatter>(@"~/Reports/FeaturesReport.txt")
            .AddFileWriter<HtmlReportFormatter>(@"~/Reports/LightBDDHtmlReport.html")
            .AddFileWriter<MarkdownReportFormatter>(@"~/Reports/LightBDDReport.md")
            ;

    }
}


public class MarkdownReportFormatter : IReportFormatter
{
    public static string WriteSubSteps(IStepResult step, string prefixStep)
    {
        var subSteps = step.GetSubSteps();
        if ((subSteps?.Count() ?? 0) == 0)
            return null;
        StringBuilder sb = new ();



        foreach (var subStep in subSteps)
        {
            string comments = string.Join(";", step.Comments);
            sb.AppendLine($"|{prefixStep}{subStep.Info.Number}|{subStep.Info.Name}|{subStep.Status}|{comments}|");

        }
        return sb.ToString();
    }
    public void Format(Stream stream, params IFeatureResult[] features)
    {
        var categories = GroupCategories(features);
        var sb = new StringBuilder();
        var parents = categories
                .Select(it => it.Info.Parent)
                .Distinct()
                .ToArray();
        sb.AppendLine("");
        
        var tests = categories
                .SelectMany(it => it.GetSteps())
                .GroupBy(it => it.Status)
                .Select(it => new { Key= it.Key.ToString(),Number= it.Count() })
                .Where(it=>it.Number != 0)
                .Select(it=>$"{it.Key}:{it.Number}")
                .ToArray();
        var data = string.Join(",", tests);
        sb.AppendLine($"# Results of tests ({data})");

        sb.AppendLine("");

        foreach (var par in parents)
        {
            var items = categories.Where(it => it.Info.Parent == par).ToArray();
            if (items.Length == 0)
                continue;
            sb.AppendLine("");
            sb.AppendLine($"## {par.Name}");
            sb.AppendLine("");
            foreach (var sc in items)
            {
                sb.AppendLine("");
                sb.AppendLine($"### {sc.Info.ToString()}");
                sb.AppendLine("");
                sb.AppendLine("| Number| Name|Status|Comments|");
                sb.AppendLine("| ----------- | ----------- |----------- |----------- |");
                foreach (var step in sc.GetSteps())
                {
                    string comments = string.Join(";", step.Comments);
                    var status = step.Status.ToString();
                    if (step.Status == ExecutionStatus.Failed)
                    {
                        status = $"<span style='color: red'>*{step.Status}*</span>";
                    }
                    var st = step.Info.Name.ToString();
                    //TODO: modify this to make more resilient
                    var name = step.Info.Name.ToString();

                    //var s=string.Join(',',step.Info.Name.Parameters.Select(it=>it.IsEvaluated));
                    if (name.Contains("<table>"))
                    {
                        string table = "";
                        foreach(var p in step.Parameters)
                        {
                            var tab = p.Details as ITabularParameterDetails;
                            if (tab == null)
                                continue;
                            table = "<table><thead><th>No</th>";
                            foreach (var col in tab.Columns)
                            {
                                table += $"<th>{col.Name}</th>";
                            }
                            table += "</thead>";
                            //table += Environment.NewLine;
                            var iRow = 1;
                            table += "<tbody>";
                            foreach (var row in tab.Rows)
                            {
                                table += $"<tr><td>{iRow++}</td>";
                                foreach(var valRow in row.Values)
                                {
                                    table += $"<td>{valRow.Value}</td>";
                                }
                                table += "</tr>";
                            }
                            table += "</tbody>";
                            table += "</table>";

                        }

                        name = name.Replace("<table>", table);
                    }
                    sb.AppendLine($"|{step.Info.Number}|{name}|{status}|{comments}|");
                    //put also step sub steps
                    var subSteps = step.GetSubSteps();
                    if ((subSteps?.Count() ?? 0) == 0)
                        continue;
                    //foreach (var subStep in subSteps)
                    //{
                    //    sb.AppendLine($"|a{subStep.Info.Number}|{subStep.Info.Name}|{subStep.Status}|");
                    //}
                    sb.Append(WriteSubSteps(step, step.Info.Number + "."));

                }
            }

        }

        using (MemoryStream ms = new())
        {
            var sw = new StreamWriter(ms);
            try
            {
                sw.Write(sb);
                sw.Flush();//otherwise you are risking empty stream
                ms.Seek(0, SeekOrigin.Begin);

                ms.WriteTo(stream);
            }
            finally
            {
                sw.Dispose();
            }
        }
    }
    private static IScenarioResult[] GroupCategories(IEnumerable<IFeatureResult> features)
    {
        var f = features
            .SelectMany(f => f.GetScenarios())
            .ToArray()
            ;
        return f;
    }
}

