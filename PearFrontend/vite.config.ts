import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import { quasar, transformAssetUrls } from "@quasar/vite-plugin";
import { fileURLToPath } from "url";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue({
      template: { transformAssetUrls },
    }),

    quasar({
      sassVariables: "src/quasar-variables.sass",
    }),
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
      "@router": fileURLToPath(new URL("./src/router", import.meta.url)),
      "@stores": fileURLToPath(new URL("./src/stores", import.meta.url)),
      "@views": fileURLToPath(new URL("./src/views", import.meta.url)),
      "@adminViews": fileURLToPath(
        new URL("./src/views/adminViews", import.meta.url)
      ),
      "@buisnessViews": fileURLToPath(
        new URL("./src/views/buisnessViews", import.meta.url)
      ),
      "@shopViews": fileURLToPath(
        new URL("./src/views/shopViews", import.meta.url)
      ),
      "@adminComponents": fileURLToPath(
        new URL("./src/components/adminComponents", import.meta.url)
      ),
      "@buisnessComponents": fileURLToPath(
        new URL("./src/components/buisnessComponents", import.meta.url)
      ),
      "@generalComponents": fileURLToPath(
        new URL("./src/components/generalComponents", import.meta.url)
      ),
      "@shopComponents": fileURLToPath(
        new URL("./src/components/shopComponents", import.meta.url)
      ),
    },
  },
});
