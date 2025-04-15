import "./page.css";

import { motion } from "framer-motion";

export default function Page({ children, className = "" }) {
  return (
    <motion.div
      className={`page ${className}`}
      initial={{ opacity: 0, x: -50 }}
      animate={{ opacity: 1, x: 0 }}
      exit={{ opacity: 0, x: 50 }}
      transition={{ duration: 0.5 }}
    >
      {children}
    </motion.div>
  );
}
