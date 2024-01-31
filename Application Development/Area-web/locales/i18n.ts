import { createI18n } from 'vue-i18n';
import English from "./en.json"
import French from "./fr.json"
import Spanish from "./es.json"

const i18n = createI18n({
  locale: "English",
  messages: {
    French,
    English,
    Spanish,
  },
});

export default i18n;
