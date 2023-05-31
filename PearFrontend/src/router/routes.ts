import HomeView from "@views/homeView.vue";
import BrowseView from "@shopViews/BrowseView.vue";
import adminView from "@adminViews/adminView.vue";
import checkoutContainer from "@shopComponents/CheckoutContainer.vue";
import CartView from "@shopViews/CartView.vue";
import registerView from "@buisnessViews/registerView.vue";
import LoginView from "@buisnessViews/LoginView.vue";
import statisticsView from "@buisnessViews/StatisticsView.vue";
import ConfigView from "@shopViews/ConfigView.vue";
import SingleProduct from "@shopComponents/SingleProductComponent.vue";
import AccountsView from "@buisnessViews/AccountsView.vue";
import OptionView from "@adminViews/OptionView.vue";

export const routes = [
  { name: "home", path: "/", component: HomeView },
  { name: "admin", path: "/admin", component: adminView },
  { name: "browse", path: "/products/:search", component: BrowseView },
  { name: "checkout", path: "/checkout", component: checkoutContainer },
  { name: "cart", path: "/cart", component: CartView },
  { name: "register", path: "/register", component: registerView },
  { name: "login", path: "/login", component: LoginView },
  { name: "chart", path: "/statistics", component: statisticsView },
  { name: "config", path: "/config", component: ConfigView },
  { name: "product", path: "/product", component: SingleProduct },
  { name: "accounts", path: "/accounts", component: AccountsView },
  { name: "option", path: "/admin/options", component: OptionView },
];
