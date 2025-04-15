import "./page.css";

import { motion } from "framer-motion";

export default function Page({ children }) {
  return (
    <motion.div
      class="page"
      initial={{ opacity: 0, x: -50 }}
      animate={{ opacity: 1, x: 0 }}
      exit={{ opacity: 0, x: 50 }}
      transition={{ duration: 0.5 }}
    >
      {children}
    </motion.div>
  );
}
