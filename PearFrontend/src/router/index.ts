import { createRouter, createWebHashHistory } from "vue-router";

import HomeView from "@views/homeView.vue";
import BrowseView from "@shopViews/BrowseView.vue";
import adminView from "@adminViews/adminView.vue";
import checkoutContainer from "@shopComponents/CheckoutContainer.vue";
import CartView from "@shopViews/CartView.vue";
import registerView from "@buisnessViews/registerView.vue";
import LoginView from "@buisnessViews/LoginView.vue";
import adminOrderView from "@buisnessViews/adminOrdersView.vue";
import statisticsView from "@buisnessViews/StatisticsView.vue";
import ConfigView from "@shopViews/ConfigView.vue";
import SingleProduct from "@shopComponents/SingleProductComponent.vue";
import AccountsView from "@buisnessViews/AccountsView.vue";
import orderStatusView from "@/views/shopViews/orderStatus.vue";
import OptionView from "@adminViews/OptionView.vue";
import useProductStore from "@/stores/ProductStore";
import useOrderStore from "@/stores/OrderStore";

// Fetches a product from the database based on it's ID
const loadProductFromId = async (to: any) => {
  const productStore = useProductStore();
  await productStore.fetchProductFromId(parseInt(to.params.id as string));
};

// Fetches all products from the database
const loadProducts = async () => {
  const productStore = useProductStore();
  await productStore.fetchProducts();
};

// Fetches all orders from the databse
const loadOrders = () => {
  const orderStore = useOrderStore();
  orderStore.fetchOrders();
};

const routes = [
  { name: "home", path: "/", component: HomeView },
  { name: "admin", path: "/admin", component: adminView },
  { name: "option", path: "/admin/options", component: OptionView },
  {
    name: "browse",
    path: "/products/:search",
    component: BrowseView,
    beforeEnter: [loadProducts],
  },
  { name: "checkout", path: "/checkout", component: checkoutContainer },
  { name: "cart", path: "/cart", component: CartView },
  { name: "register", path: "/register", component: registerView },
  { name: "login", path: "/login", component: LoginView },
  { name: "chart", path: "/statistics", component: statisticsView },
  { name: "config", path: "/config", component: ConfigView },
  {
    name: "product",
    path: "/product/:id",
    component: SingleProduct,
    beforeEnter: [loadProductFromId],
  },
  { name: "adminOrder", path: "/adminOrder", component: adminOrderView },
  { name: "accounts", path: "/accounts", component: AccountsView },
  {
    name: "order",
    path: "/order/:id",
    component: orderStatusView,
    beforeEnter: [loadOrders],
  },
];
const router = createRouter({
  history: createWebHashHistory(),
  routes: routes,
});
export default router;
