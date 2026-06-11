import { OwnerList } from '../components/OwnerList'
import { PetList } from '../components/PetList'
import { VetList } from '../components/VetList'
import { AppointmentList } from '../components/AppointmentList'

export function OwnersPage() {
  return <OwnerList />
}

export function PetsPage() {
  return <PetList />
}

export function VetsPage() {
  return <VetList />
}

export function AppointmentsPage() {
  return <AppointmentList />
}
