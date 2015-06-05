using Inventory.Core.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using InventorySolution.Models;

namespace InventorySolution
{
    public static class Helper
    {
        public static string NoData
        {
            get
            {
                var content = string.Empty;
                var div = new TagBuilder("div");
                div.SetInnerText("FARA INFORMATII");
                content += div.ToString(TagRenderMode.Normal);

                return content;
            }
        }

        public static string ToLink(this string value, string url = "", IDictionary<string, string> attributes = null,
            bool replaceExisting = true)
        {
            var link = new TagBuilder("a");
            link.MergeAttribute("href", url, replaceExisting);
            link.InnerHtml = value;
            if (attributes != null)
            {
                link.MergeAttributes(attributes, replaceExisting);
            }
            return link.ToString(TagRenderMode.Normal);
        }

        public static MvcHtmlString ShowInventare(IList<InventarDataModel> inventare)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var content = string.Empty;
            //daca avem inventare, facem un tabel, altfel afisam mesaj
            if (inventare != null && inventare.Count > 0)
            {
                var table = new TagBuilder("table");
                table.MergeAttribute("class", "table");

                var thead = new TagBuilder("thead");
                thead.InnerHtml = String.Empty;

                var tbody = new TagBuilder("tbody");
                tbody.InnerHtml = String.Empty;

                var tr = new TagBuilder("tr");
                tr.InnerHtml = String.Empty;

                #region head

                //id
                var th = new TagBuilder("th");
                th.InnerHtml = "Nr_ctr";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //denumire
                th = new TagBuilder("th");
                th.InnerHtml = "Denumire";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //nr inventar
                th = new TagBuilder("th");
                th.InnerHtml = "Nr inventar";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //nume
                th = new TagBuilder("th");
                th.InnerHtml = "Nume";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //laborator
                th = new TagBuilder("th");
                th.InnerHtml = "Laborator";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //sursa
                th = new TagBuilder("th");
                th.InnerHtml = "Sursa";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //tip
                th = new TagBuilder("th");
                th.InnerHtml = "Tip";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //an produs functionalitate
                th = new TagBuilder("th");
                th.InnerHtml = "AnPFun";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //proces verbal
                th = new TagBuilder("th");
                th.InnerHtml = "PVerbal";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //serie
                th = new TagBuilder("th");
                th.InnerHtml = "Serie";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //pret
                th = new TagBuilder("th");
                th.InnerHtml = "Pret";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //cantitate
                th = new TagBuilder("th");
                th.InnerHtml = "Cantitate";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //valoare
                th = new TagBuilder("th");
                th.InnerHtml = "Valoare";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);
                #endregion

                thead.InnerHtml += tr.ToString(TagRenderMode.Normal);
                table.InnerHtml = thead.ToString(TagRenderMode.Normal);

                #region body
                foreach (var inventar in inventare)
                {
                    tr = new TagBuilder("tr");
                    tr.InnerHtml = String.Empty;

                    //id
                    //creeaza link de editare inventar
                    var link = new TagBuilder("a");
                    link.MergeAttribute("href",
                        (inventar.InventarId > 0 ? urlHelper.Action("Details", "Inventare", new { id = inventar.InventarId}) : string.Empty));
                    link.SetInnerText(string.Format("#{0}", inventar.InventarId));

                    var td = new TagBuilder("td");
                    td.InnerHtml = link.ToString(TagRenderMode.Normal);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //denumire
                    td = new TagBuilder("td");
                    td.InnerHtml = inventar.Denumire;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //nr inventar
                    td = new TagBuilder("td");
                    td.InnerHtml = inventar.NrInventar;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //nume + prenume
                    //link de editare gestiune
                    link = new TagBuilder("a");
                    link.MergeAttribute("href", urlHelper.Action("Index", "Gestiuni"));
                    link.SetInnerText(
                        inventar.Gestiune == null ? String.Format("#{0}", inventar.Gestiune.Id)
                        : string.Format("{0} {1}", inventar.Gestiune.Nume, inventar.Gestiune.Prenume));

                    td = new TagBuilder("td");
                    td.InnerHtml = link.ToString(TagRenderMode.Normal);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //laborator
                    //link de editare laborator
                    link = new TagBuilder("a");
                    link.MergeAttribute("href", urlHelper.Action("Index", "Laboratoare"));
                    link.SetInnerText(
                        inventar.Laborator == null ? String.Format("#{0}", inventar.Laborator.Id)
                        : string.Format("{0}", inventar.Laborator.Nume));

                    td = new TagBuilder("td");
                    td.InnerHtml = link.ToString(TagRenderMode.Normal);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //sursa
                    //link de editare sursa
                    link = new TagBuilder("a");
                    link.MergeAttribute("href", urlHelper.Action("Index", "Surse"));
                    link.SetInnerText(
                        inventar.Sursa == null ? String.Format("#{0}", inventar.Sursa.Id)
                        : string.Format("{0}", inventar.Sursa.Nume));

                    td = new TagBuilder("td");
                    td.InnerHtml = link.ToString(TagRenderMode.Normal);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //tip
                    //link de editare tip
                    link = new TagBuilder("a");
                    link.MergeAttribute("href",
                        inventar.Tip.Id > 0 ? urlHelper.Action("Details", "Tipi", new { id = inventar.Tip.Id }) : String.Empty);
                    link.SetInnerText(
                        inventar.Tip == null ? String.Format("#{0}", inventar.Tip.Id)
                        : string.Format("{0}", inventar.Tip.Nume));

                    td = new TagBuilder("td");
                    td.InnerHtml = link.ToString(TagRenderMode.Normal);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //an produs functionalitate
                    td = new TagBuilder("td");
                    td.InnerHtml = inventar.AnPFun;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //proces verbal
                    td = new TagBuilder("td");
                    td.InnerHtml = inventar.PVerbal;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //serie
                    td = new TagBuilder("td");
                    td.InnerHtml = inventar.Serie;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //pret
                    td = new TagBuilder("td");
                    td.MergeAttribute("style", "text-align:right");
                    td.InnerHtml = inventar.Pret.HasValue ? string.Format("{0} lei", inventar.Pret.Value) : String.Empty;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //cantitate
                    td = new TagBuilder("td");
                    td.MergeAttribute("style", "text-align:right");
                    td.InnerHtml = inventar.Cantitate.HasValue ? string.Format("{0}", inventar.Cantitate.Value) : String.Empty;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //valoare
                    td = new TagBuilder("td");
                    td.MergeAttribute("style", "text-align:right");
                    td.InnerHtml = inventar.Valoare.HasValue ? string.Format("{0:F} lei", inventar.Valoare.Value) : String.Empty;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //adauga rand la tabel
                    table.InnerHtml += tr.ToString(TagRenderMode.Normal);
                }
                #endregion

                table.InnerHtml += tbody.ToString(TagRenderMode.Normal);
                content += table.ToString(TagRenderMode.Normal);
            }
            else
            {
                content = Helper.NoData;
            }
            return MvcHtmlString.Create(content);
        }

        public static object ShowCasare(List<CasareModel> casare)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var content = string.Empty;
            //daca avem inventare, facem un tabel, altfel afisam mesaj
            if (casare != null && casare.Count > 0)
            {
                var table = new TagBuilder("table");
                table.MergeAttribute("class", "table");

                var thead = new TagBuilder("thead");
                thead.InnerHtml = String.Empty;

                var tbody = new TagBuilder("tbody");
                tbody.InnerHtml = String.Empty;

                var tr = new TagBuilder("tr");
                tr.InnerHtml = String.Empty;

                #region head

                //id
                var th = new TagBuilder("th");
                th.InnerHtml = "Nr_ctr";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //denumire
                th = new TagBuilder("th");
                th.InnerHtml = "Denumire";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //nume
                th = new TagBuilder("th");
                th.InnerHtml = "Nume";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //laborator
                th = new TagBuilder("th");
                th.InnerHtml = "Laborator";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //an produs functionalitate
                th = new TagBuilder("th");
                th.InnerHtml = "AnPFun";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //proces verbal
                th = new TagBuilder("th");
                th.InnerHtml = "PVerbal";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //numar inventar
                th = new TagBuilder("th");
                th.InnerHtml = "NrInventar";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //serie
                th = new TagBuilder("th");
                th.InnerHtml = "Serie";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //pret
                th = new TagBuilder("th");
                th.InnerHtml = "Pret";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //cantitate
                th = new TagBuilder("th");
                th.InnerHtml = "Cantitate";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //valoare
                th = new TagBuilder("th");
                th.InnerHtml = "Valoare";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //mentiuni
                th = new TagBuilder("th");
                th.InnerHtml = "Mentiuni";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);

                //data casare
                th = new TagBuilder("th");
                th.InnerHtml = "DataCasare";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);
                
                //cod
                th = new TagBuilder("th");
                th.InnerHtml = "Cod";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);
                
                //durata normala
                th = new TagBuilder("th");
                th.InnerHtml = "DurataNormala";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);
                
                //durata reala
                th = new TagBuilder("th");
                th.InnerHtml = "DurataReala";
                tr.InnerHtml += th.ToString(TagRenderMode.Normal);
                #endregion

                thead.InnerHtml += tr.ToString(TagRenderMode.Normal);
                table.InnerHtml = thead.ToString(TagRenderMode.Normal);

                #region body
                foreach (var cas in casare)
                {
                    tr = new TagBuilder("tr");
                    tr.InnerHtml = String.Empty;

                    //id
                    //creeaza link de editare inventar
                    var link = new TagBuilder("a");
                    link.MergeAttribute("href",
                        (cas.Id > 0 ? urlHelper.Action("Details", "Inventare", new { id = cas.Id }) : string.Empty));
                    link.SetInnerText(string.Format("#{0}", cas.Id));

                    var td = new TagBuilder("td");
                    td.InnerHtml = link.ToString(TagRenderMode.Normal);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //denumire
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Denumire;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //nume + prenume                      

                    td = new TagBuilder("td");
                    td.InnerHtml = string.Format("{0} {1}", cas.Nume, cas.Prenume);
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //laborator
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Laborator;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //an produs functionalitate
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.AnPFun;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //proces verbal
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.PVerbal;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //numar inventar
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.NrInventar;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //serie
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Serie;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //pret
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Pret.ToString();
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //cantitate
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Cantitate.ToString();
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //valoare
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Valoare.ToString();
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //mentiuni
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Mentiuni;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //data casare
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.DataCasare;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //cod
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.Cod;
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //durata normala
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.DurataNormala.ToString();
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //durata reala
                    td = new TagBuilder("td");
                    td.InnerHtml = cas.DurataReal.ToString();
                    tr.InnerHtml += td.ToString(TagRenderMode.Normal);

                    //adauga rand la tabel
                    table.InnerHtml += tr.ToString(TagRenderMode.Normal);
                }
                #endregion

                table.InnerHtml += tbody.ToString(TagRenderMode.Normal);
                content += table.ToString(TagRenderMode.Normal);
            }
            else
            {
                content = Helper.NoData;
            }
            return MvcHtmlString.Create(content);
        }
    }
}