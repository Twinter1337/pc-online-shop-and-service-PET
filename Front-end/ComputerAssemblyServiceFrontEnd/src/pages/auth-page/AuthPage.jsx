import Page from "../Page/Page";
import AuthForm from "./AuthForm/AuthForm";
import "./AuthPage.css";

export default function AuthPage() {
  return (
    <Page className="auth-page">
      <AuthForm />
    </Page>
  );
}
