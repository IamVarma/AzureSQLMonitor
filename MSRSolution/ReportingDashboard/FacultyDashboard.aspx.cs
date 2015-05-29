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

public partial class FacultyDashboard : System.Web.UI.Page
{
    string conn = ConfigurationManager.ConnectionStrings["MECProdConnectionString"].ConnectionString;
    string courselist = "";
    //have to use userid in place of 155
    int instructor = 9369;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCourse(instructor);
            GetCourseList();
            
            DataTable dt = GetRegisteredCourse(instructor,courselist);
            DataTable rt = GetRegistrationTmln(instructor, courselist);
            DataTable cp = GetCourseCompletion(instructor, courselist);
            DataTable cc = GetCourseCompletionTrend(instructor, courselist);
            DataTable kt = GetScore(instructor, courselist);
            DataTable ht = GetKCTimeLine(instructor, courselist);
            DataTable hier = GetHierarchy(instructor, courselist);
            CheckAllCourses();
            ReportViewer1.Visible = true;
            ReportViewer1.LocalReport.ReportPath = @"FacultyDashboard.rdlc";

            ReportParameter p1 = new ReportParameter("Color", ddl_color.SelectedValue.ToString());
            this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RegisteredCourse", dt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RegistrationTimeLines", rt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseCompletion", cp));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseCompletionTrend", cc));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ScorePerCourse", kt));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("QuizTimeline", ht));
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InstructorHierarchy", hier));
            ReportViewer1.LocalReport.Refresh();
        }
    }
    private void LoadCourse(int inst)
    {
        chkCourse.Items.Clear();
        DataSet ds = new DataSet();
        SqlConnection cn = new SqlConnection(conn);
        cn.Open();
        SqlCommand cmd = new SqlCommand("rpt_InsGetCourseList", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        // Replace value with Userid captured from login
        cmd.Parameters.Add("@TeacherID", SqlDbType.Int, 20).Value = inst;        
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds);
        DataTable dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            chkCourse.DataTextField = "ProductName";
            chkCourse.DataValueField = "courseid";
            chkCourse.DataSource = dt;
            chkCourse.DataBind();
        }
        if (chkCourse.Items.Count == 0)
        {
            lblcourse.Text = "No Course to display";
            lblcourse.Visible = true;
        }
    }
    private void CheckAllCourses()
    {
        for (int i = 0; i < chkCourse.Items.Count; i++)
        {

            chkCourse.Items[i].Selected = true;
        }


    }
    private string GetCourseList()
    {
        foreach (ListItem item in chkCourse.Items)
        {
            courselist += item.Value + ",";
        }
        if (courselist.Length > 0)
            courselist = courselist.Substring(0, courselist.Length - 1);
        return courselist;
    }
    private string GetSelectedCourseList()
    {

        foreach (ListItem item in chkCourse.Items)
        {
            if (item.Selected)
                courselist += item.Value + ",";
        }
        if (courselist.Length > 0)
            courselist = courselist.Substring(0, courselist.Length - 1);
        return courselist;
    }
    private DataTable GetRegisteredCourse(int inst,string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetRegisteredCourse", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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
    private DataTable GetRegistrationTmln(int inst, string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetRegistrationTimeline", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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
    private DataTable GetCourseCompletion(int inst, string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetCourseCompletion", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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

    private DataTable GetCourseCompletionTrend(int inst, string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetCourseCompletionTrend", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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
    private DataTable GetScore(int inst, string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetQuizScore", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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
    private DataTable GetKCTimeLine(int inst, string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetQuizScoreTimeLine", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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
    private DataTable GetHierarchy(int inst, string clist)
    {
        SqlConnection cn = new SqlConnection(conn);
        DataTable ComPerc = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("rpt_InsGetInstructorHierarchy", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InstructorID", inst);
            cmd.Parameters.AddWithValue("@Course", clist);
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        courselist = "";
        GetSelectedCourseList();
        ReportParameter p1 = new ReportParameter("Color", ddl_color.SelectedValue.ToString());
        this.ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { p1 });

        DataTable dt = GetRegisteredCourse(instructor, courselist);
        DataTable rt = GetRegistrationTmln(instructor, courselist);
        DataTable cp = GetCourseCompletion(instructor, courselist);
        DataTable cc = GetCourseCompletionTrend(instructor, courselist);
        DataTable kt = GetScore(instructor, courselist);
        DataTable ht = GetKCTimeLine(instructor, courselist);
        DataTable hier = GetHierarchy(instructor, courselist);        
        ReportViewer1.Visible = true;
        ReportViewer1.LocalReport.ReportPath = @"FacultyDashboard.rdlc";
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RegisteredCourse", dt));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("RegistrationTimeLines", rt));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseCompletion", cp));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseCompletionTrend", cc));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ScorePerCourse", kt));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("QuizTimeline", ht));
        ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("InstructorHierarchy", hier));
        ReportViewer1.LocalReport.Refresh();
    }
}