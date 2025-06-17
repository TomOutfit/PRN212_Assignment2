using System;
using System.Linq; // Cần thiết cho .Any()
using System.Windows;
using System.Windows.Controls;
using BusinessObjects; // Đảm bảo đã thêm reference đến BusinessObjects
using Services;       // Đảm bảo đã thêm reference đến Services

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Sử dụng interface để dependency injection tốt hơn
        private readonly IProductService _iProductService;
        private readonly ICategoryService _iCategoryService;

        public MainWindow()
        {
            InitializeComponent();
            // Khởi tạo các Service. Các Service này bên trong sẽ gọi đến các DAO
            // mà chúng ta đã cập nhật để tương tác với Database thay vì JSON.
            _iProductService = new ProductService();
            _iCategoryService = new CategoryService();
        }

        /// <summary>
        /// Tải danh sách Categories và hiển thị lên ComboBox.
        /// </summary>
        public void LoadCategoryList()
        {
            try
            {
                var catList = _iCategoryService.GetCategories();
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName"; // Hiển thị tên Category
                cboCategory.SelectedValuePath = "CategoryId";   // Giá trị thực tế khi chọn là ID
                // Chọn category đầu tiên nếu có, nếu không thì không chọn gì (-1)
                cboCategory.SelectedIndex = catList.Any() ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách danh mục: {ex.Message}", "Lỗi tải danh mục", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Tải danh sách Products và hiển thị lên DataGrid.
        /// </summary>
        public void LoadProductList()
        {
            try
            {
                dgData.ItemsSource = null; // Xóa ItemsSource để làm mới DataGrid
                var productList = _iProductService.GetProducts();
                dgData.ItemsSource = productList; // Gán danh sách sản phẩm mới
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách sản phẩm: {ex.Message}", "Lỗi tải sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                resetInput(); // Luôn reset các trường nhập liệu sau khi tải
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi cửa sổ được tải.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList(); // Tải danh mục trước
            LoadProductList();  // Sau đó tải sản phẩm
        }

        /// <summary>
        /// Xử lý sự kiện click cho nút "Create".
        /// </summary>
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra và validate đầu vào
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
                {
                    MessageBox.Show("Giá phải là một số hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!short.TryParse(txtUnitsInStock.Text.Trim(), out short unitsInStock) || unitsInStock < 0)
                {
                    MessageBox.Show("Số lượng trong kho phải là một số nguyên hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn một danh mục.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Product product = new Product
                {
                    // ProductID sẽ được tạo tự động bởi tầng DAO/Service khi lưu vào database
                    // (Nếu cột ProductID trong database được cấu hình là Identity)
                    ProductId = 0, // Đặt là 0 để báo hiệu đây là sản phẩm mới và sẽ tự động tạo ID
                    ProductName = txtProductName.Text.Trim(),
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    CategoryId = (int)cboCategory.SelectedValue // Ép kiểu an toàn
                };

                _iProductService.SaveProduct(product); // Gọi Service để lưu sản phẩm
                MessageBox.Show("Sản phẩm đã được tạo thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo sản phẩm: {ex.Message}", "Lỗi tạo sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProductList(); // Làm mới danh sách sản phẩm sau khi tạo
            }
        }

        /// <summary>
        /// Xử lý sự kiện thay đổi lựa chọn hàng trong DataGrid.
        /// </summary>
        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn và đó có phải là Product không
            if (dgData.SelectedItem is Product selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductId.ToString();
                txtProductName.Text = selectedProduct.ProductName ?? string.Empty; // Xử lý null
                txtPrice.Text = selectedProduct.UnitPrice.ToString("F2") ?? string.Empty; // Định dạng tiền tệ
                txtUnitsInStock.Text = selectedProduct.UnitsInStock.ToString() ?? string.Empty; // Xử lý null
                cboCategory.SelectedValue = selectedProduct.CategoryId; // Set giá trị cho ComboBox
            }
            else
            {
                resetInput(); // Nếu không có gì được chọn, reset các trường nhập liệu
            }
        }

        /// <summary>
        /// Xử lý sự kiện click cho nút "Close".
        /// </summary>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Đóng cửa sổ hiện tại
        }

        /// <summary>
        /// Xử lý sự kiện click cho nút "Update".
        /// </summary>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra xem Product ID có hợp lệ không (để đảm bảo đã chọn sản phẩm)
                if (!int.TryParse(txtProductID.Text, out int productId) || productId <= 0)
                {
                    MessageBox.Show("Bạn phải chọn một Sản phẩm để cập nhật!", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Tiếp tục validate các trường khác
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Tên sản phẩm không được để trống.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
                {
                    MessageBox.Show("Giá phải là một số hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!short.TryParse(txtUnitsInStock.Text.Trim(), out short unitsInStock) || unitsInStock < 0)
                {
                    MessageBox.Show("Số lượng trong kho phải là một số nguyên hợp lệ không âm.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn một danh mục.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Product product = new Product
                {
                    ProductId = productId, // Sử dụng ID từ TextBox để cập nhật
                    ProductName = txtProductName.Text.Trim(),
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    CategoryId = (int)cboCategory.SelectedValue
                };

                _iProductService.UpdateProduct(product); // Gọi Service để cập nhật sản phẩm
                MessageBox.Show("Sản phẩm đã được cập nhật thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException ex) // Bắt lỗi cụ thể từ tầng Repository/DAO (ví dụ: sản phẩm không tồn tại)
            {
                MessageBox.Show($"Cập nhật thất bại: {ex.Message}", "Lỗi cập nhật sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật sản phẩm: {ex.Message}", "Lỗi cập nhật sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProductList(); // Làm mới danh sách sản phẩm
            }
        }

        /// <summary>
        /// Xử lý sự kiện click cho nút "Delete".
        /// </summary>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra xem Product ID có hợp lệ không
                if (!int.TryParse(txtProductID.Text, out int productIdToDelete) || productIdToDelete <= 0)
                {
                    MessageBox.Show("Bạn phải chọn một Sản phẩm để xóa!", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Lấy thông tin sản phẩm để hiển thị trong hộp thoại xác nhận
                Product productToDelete = _iProductService.GetProductById(productIdToDelete);
                if (productToDelete != null)
                {
                    MessageBoxResult result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn xóa Sản phẩm ID: {productToDelete.ProductId} - {productToDelete.ProductName}?",
                        "Xác nhận xóa",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question
                    );

                    if (result == MessageBoxResult.Yes)
                    {
                        _iProductService.DeleteProduct(productToDelete); // Gọi Service để xóa sản phẩm
                        MessageBox.Show("Sản phẩm đã được xóa thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm để xóa.", "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Xóa thất bại: {ex.Message}", "Lỗi xóa sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa sản phẩm: {ex.Message}", "Lỗi xóa sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProductList(); // Làm mới danh sách sản phẩm
            }
        }

        /// <summary>
        /// Đặt lại các trường nhập liệu về trạng thái ban đầu.
        /// </summary>
        private void resetInput()
        {
            txtProductID.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtUnitsInStock.Text = string.Empty;
            cboCategory.SelectedIndex = -1; // Bỏ chọn ComboBox
        }
    }
}