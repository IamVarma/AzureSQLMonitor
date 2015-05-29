using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
 

public partial class _Default : System.Web.UI.Page
{
    string conn = ConfigurationManager.ConnectionStrings["MECProdConnectionString"].ConnectionString;
    string courselist = "";
    //have to use userid in place of 11501
    string student = "11501";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCourse("1");
            GetCourseList();
            ReportParameter p1 = new ReportParameter("Color", ddl_color.SelectedValue.ToString());            
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1});

            DataTable dt = GetRegisteredCount();
            DataTable sp = GetScorePercentage(student,courselist);
            DataTable cp = GetCompletionPercentage(student, courselist);
            DataTable ca = GetCourseAverage(student, courselist);
            DataTable kt = GetKCTrend(student, courselist);
            DataTable hier = GetHierarchy(student, courselist);
            CheckAllCourses();
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = @"StudentDashboard.rdlc";
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RegisteredCourse", dt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ScorePercentage", sp));            
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CompletionPercentage", cp));
            //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CompletionPercentage", ca));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CollegewisecourseAvg", ca));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("KCTrendPerCourse", kt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Hierarchy", hier));
            ReportViewer1.LocalReport.Refresh();
        }            
        }
    private void CheckAllCourses()
       {
           for (int i = 0; i < CheckBoxList1.Items.Count; i++)
           {

               CheckBoxList1.Items[i].Selected = true;
           }


    }

    private DataTable GetRegisteredCount()
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ResultsTable = new DataTable();
        try
        {   
            SqlCommand cmd = new SqlCommand("rpt_GetRegisteredCompletedCourses", cn);
            cmd.CommandType = CommandType.StoredProcedure;           
            cmd.Parameters.AddWithValue("@Student", 11501);
            //cmd.Parameters.AddWithValue("@Course",27001);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ResultsTable);
        }

        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
            }
        }

        return ResultsTable;
    }
    private DataTable GetScorePercentage(string student, string list)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ScorePer = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_GetScorePercentage", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Student", student);
            cmd.Parameters.AddWithValue("@Course",list);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ScorePer);
        }

        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
            }
        }

        return ScorePer;
    }
    private void LoadCourse(string status)
    {
        CheckBoxList1.Items.Clear();
        DataSet ds = new DataSet();
        
        SqlConnection cn = new SqlConnection(conn);
        cn.Open();
        SqlCommand cmd = new SqlCommand("rpt_GetCourseForStudent", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        // Replace value with Userid captured from login
        cmd.Parameters.Add("@Student", SqlDbType.VarChar, 20).Value = student;
        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = status;
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds);
        DataTable dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            CheckBoxList1.DataTextField = "CourseName";
            CheckBoxList1.DataValueField = "courseid";
            CheckBoxList1.DataSource = dt;
            CheckBoxList1.DataBind();
        }
        if (CheckBoxList1.Items.Count == 0)
        {
            lblcourse.Text = "No Course to display";
            lblcourse.Visible = true;
        }
    }
    private string GetCourseList()
    {
        
        foreach (ListItem item in CheckBoxList1.Items)
        {
            courselist += item.Value + ",";
        }
        if (courselist.Length > 0)
            courselist = courselist.Substring(0, courselist.Length - 1);
        return courselist;
    }
    private string GetSelectedCourseList()
    {

        foreach (ListItem item in CheckBoxList1.Items)
        {
            if (item.Selected)
            courselist += item.Value + ",";
        }
        if (courselist.Length > 0)
            courselist = courselist.Substring(0, courselist.Length - 1);
        return courselist;
    }
   
    protected void cbl_CourseStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string courseStatus = "";

        foreach (ListItem item in cbl_CourseStatus.Items)
            if (item.Selected)
                courseStatus += item.Value + ",";
        if(courseStatus.Length>0)
        courseStatus = courseStatus.Substring(0, courseStatus.Length - 1);
        if (courseStatus == "")
            courseStatus = "1";
        LoadCourse(courseStatus);
    }

    private DataTable GetCompletionPercentage(string student,string list)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_GetCompletionPercentage", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Student", student);
            cmd.Parameters.AddWithValue("@Course", list);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ComPerc);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
            }
        }
        return ComPerc;
    }

    private DataTable GetCourseAverage(string student, string list)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable CourseAvg = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_GetCourseAvg", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Student", student);
            cmd.Parameters.AddWithValue("@Course", list);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(CourseAvg);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
            }
        }
        return CourseAvg;
    }

    private DataTable GetKCTrend(string student, string list)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable CourseAvg = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_KCTrendPerCourse", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Student", student);
            cmd.Parameters.AddWithValue("@Course", list);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(CourseAvg);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
            }
        }
        return CourseAvg;
    }

    private DataTable GetHierarchy(string student, string list)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable CourseAvg = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_GetUserDetails", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Student", student);
            cmd.Parameters.AddWithValue("@Course", list);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(CourseAvg);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (cn != null)
            {
                cn.Close();
            }
        }
        return CourseAvg;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        courselist = "";
        //LoadCourse("1");
        GetSelectedCourseList();
        ReportParameter p1 = new ReportParameter("Color", ddl_color.SelectedValue.ToString());
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

        DataTable dt = GetRegisteredCount();
        DataTable sp = GetScorePercentage(student, courselist);
        DataTable cp = GetCompletionPercentage(student, courselist);
        DataTable ca = GetCourseAverage(student, courselist);
        DataTable kt = GetKCTrend(student, courselist);
        DataTable hier = GetHierarchy(student, courselist);
        //CheckAllCourses();
        ReportViewer1.Visible = true;
        ReportViewer1.LocalReport.ReportPath = @"StudentDashboard.rdlc";
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RegisteredCourse", dt));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ScorePercentage", sp));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CompletionPercentage", cp));
        //ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CompletionPercentage", ca));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CollegewisecourseAvg", ca));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("KCTrendPerCourse", kt));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Hierarchy", hier));
        ReportViewer1.LocalReport.Refresh();
    }
}
  
    
