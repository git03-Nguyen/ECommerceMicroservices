import { Navigate, Outlet, useLocation } from "react-router-dom";
import { useAppSelector } from "../store/configureStore";
import { toast } from "react-toastify";

interface Props {
  role?: string;
}

export default function RequireAuth({ role }: Props) {
  const { user } = useAppSelector((state) => state.account);
  const location = useLocation();

  if (!user) {
    return <Navigate to="/login" state={{ from: location }} />;
  }

  if (role && !role.includes(user.role!)) {
    toast.error("Not authorised to access this area");
    return <Navigate to="/catalog" />;
  }

  return <Outlet />;
}
