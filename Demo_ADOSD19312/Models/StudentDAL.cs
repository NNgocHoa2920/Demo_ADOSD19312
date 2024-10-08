using System.Data.SqlClient;

namespace Demo_ADOSD19312.Models
{
    //tầng này tương tác trực tiếp vs database , công việc sẽ liên quan đến crud
    public class StudentDAL
    {
        //khởi tạo chuỗi kết nối
        //nếu chuỗi của các bạn có dấu \ thì sẽ cs 2 cách fix
        //cách 1: thêm @ đằng trước "
        //cách 2: thêm 1 \ tại dấu \
        
        string connectionString  = "Server=NGUYEN_NGOC_HOA\\HOANN;Database=DemoADO;Trusted_Connection=True;TrustServerCertificate=True";
        

        //Hiển thị toàn bộ dữ liệu sinh viên
        //IEnumerable: dùng để chỉ đọc
        public IEnumerable<Student> GetAllStudent()
        {
            //var: khi chưa xác định đc kiểu dữ liệu
            //thích hợp cho khai báo đôi tượng 
            var lstStudent = new List<Student>();
            //using được sử dụng theo 2 cách
            //cách 1: để khai báo các thư viện hoặc packget
            //các 2: đảm bả rằng  1 hay nhiều câ lệnh sẽ đc dừng tự động sau khi hoàn thành
            //SqlConnection: nó là 1 đối tượng dùng để kết nối vs db
            //Nó có 2 trạng thái: open() và close()
            //nếu k ns gì thì nó sẽ mặc định là close
            //vậy nên khi kết nối các bạn open ra
            //và dùng sau thì close lại => đảm bảo kiểm soát đc trạng thái của nó
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM STUDENT";
                //sql command là 1 đối tượng dùng để thực thi câu lệnh sql
                //có 2 tham số
                //tham só 1: câu lệnh sql
                //tham số 2: chuỗi kết nối
                SqlCommand cmd = new SqlCommand(query, con);
                //sql datareader: dùng để đọc dữ liệu từ csdl => chỉ đọc
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) 
                { 
                    Student student = new Student();
                    student.Id = Convert.ToInt32(reader["ID"]);
                    student.Name = reader["Name"].ToString();
                    student.Gender = reader["Gender"].ToString();
                    lstStudent.Add(student);// mình add dữ liệu ms lấy ở sql vào list để mình hiern thị

                }
                con.Close();

            }
            return lstStudent;
        }

        //thêm student
        public void AddStudent(Student st)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO STUDENT(ID, NAME, GENDER) VALUES (@ID,@NAME,@GENDER)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ID", st.Id);
                cmd.Parameters.AddWithValue("@NAME", st.Name);
                cmd.Parameters.AddWithValue("@GENDER", st.Gender);

                cmd.ExecuteNonQuery();// ĐỂ THỰC THI CÁC CÂU LỆNH NHƯ INSERT, UDATE, DELETE
                con.Close();
            }
        }

        //sửa
        public void UpdateSudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE STUDENT SET NAME=@NAME , GENDER = @GENDER WHERE ID= @ID";
                SqlCommand cmd = new SqlCommand(query, con);

                //thêm các tham số cho câu lệnh
                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Gender", student.Gender);

                //thực
                cmd.ExecuteNonQuery();
                con.Close();
            }    
        }

        //tìm kiếm theo id
        public Student GetStudentByID(int Id)
        {
            Student st = new Student();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT * FROM STUDENT WHERE ID=@ID";

                SqlCommand cmd = new SqlCommand(query, con);
                //thêm tham số cho câu lệnh
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    st = new Student
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        Name = reader["Name"].ToString(),
                        Gender = reader["Gender"].ToString()
                    };
                }
                con.Close();
            }
            return st;
        }

        //xóa
        public void DeleteST(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM STUDENT WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
