using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        private readonly IProductService _iProductService;
        private readonly ICategoryService _iCategoryService;

        public MainWindow()
        {
            InitializeComponent();
            _iProductService = new ProductService();
            _iCategoryService = new CategoryService();
        }

        public void LoadCategoryList()
        {
            try
            {
                var catList = _iCategoryService.GetCategories();
                cboCategory.ItemsSource = null; // Clear to avoid binding issues
                cboCategory.ItemsSource = catList;
                cboCategory.DisplayMemberPath = "CategoryName";
                cboCategory.SelectedValuePath = "CategoryId";
                cboCategory.SelectedIndex = catList.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void LoadProductList()
        {
            try
            {
                dgData.ItemsSource = null; // Clear to avoid binding issues
                var productList = _iProductService.GetProducts();
                dgData.ItemsSource = productList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                resetInput();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Product name is required.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
                {
                    MessageBox.Show("Price must be a valid non-negative number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!short.TryParse(txtUnitsInStock.Text.Trim(), out short unitsInStock) || unitsInStock < 0)
                {
                    MessageBox.Show("Units In Stock must be a valid non-negative number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var products = _iProductService.GetProducts();
                int newId = products.Count > 0 ? products.Max(p => p.ProductID) + 1 : 1;
                Product product = new Product
                {
                    ProductID = newId,
                    ProductName = txtProductName.Text.Trim(),
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    CategoryId = int.Parse(cboCategory.SelectedValue.ToString())
                };

                _iProductService.SaveProduct(product);
                MessageBox.Show("Product created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Product selectedProduct)
            {
                txtProductID.Text = selectedProduct.ProductID.ToString();
                txtProductName.Text = selectedProduct.ProductName ?? "";
                txtPrice.Text = selectedProduct.UnitPrice.ToString("F2") ?? "";
                txtUnitsInStock.Text = selectedProduct.UnitsInStock.ToString() ?? "";
                cboCategory.SelectedValue = selectedProduct.CategoryId;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtProductID.Text, out int ProductID))
                {
                    MessageBox.Show("You must select a product to update!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Product name is required.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal unitPrice) || unitPrice < 0)
                {
                    MessageBox.Show("Price must be a valid non-negative number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!short.TryParse(txtUnitsInStock.Text.Trim(), out short unitsInStock) || unitsInStock < 0)
                {
                    MessageBox.Show("Units In Stock must be a valid non-negative number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Product product = new Product
                {
                    ProductID = ProductID,
                    ProductName = txtProductName.Text.Trim(),
                    UnitPrice = unitPrice,
                    UnitsInStock = unitsInStock,
                    CategoryId = int.Parse(cboCategory.SelectedValue.ToString())
                };

                _iProductService.UpdateProduct(product);
                MessageBox.Show("Product updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtProductID.Text, out int ProductID))
                {
                    MessageBox.Show("You must select a product to delete!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Product product = _iProductService.GetProductById(ProductID);
                if (product != null)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete '{product.ProductName}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _iProductService.DeleteProduct(product);
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Product not found!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void resetInput()
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtUnitsInStock.Text = "";
            cboCategory.SelectedIndex = -1;
        }
    }
}