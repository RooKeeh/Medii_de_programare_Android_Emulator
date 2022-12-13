using Moldovan_Andrei_Lab7.Models;

namespace Moldovan_Andrei_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var shopl = (ShopList)BindingContext;

        listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        Product product;
        var shopList = (ShopList)BindingContext;
        if(listView.SelectedItem != null)
        {
            product = listView.SelectedItem as Product;

            var listProductAll = await App.Database.GetListProducts();

            var listProduct = listProductAll.FindAll(x => x.ProductID == product.ID & x.ShopListID == shopList.ID);

            await App.Database.DeleteListProductAsync(listProduct.FirstOrDefault());
            await Navigation.PopAsync();
        }
    }
}