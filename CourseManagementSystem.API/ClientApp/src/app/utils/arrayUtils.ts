/**
 * class containing additional array methods
 */
export class ArrayUtils {

  /**
   * resize array to the given size (modifies the array!)
   * - if the given size is smaller or equal to the current size, resize array to the first n items
   * - otherwise extend it to the given size - add copies of {@instanceToAdd} at the end
   * @param array given array
   * @param newSize new size of the array
   * @param instanceToAdd instance to add to the array if `newSize > array.length`
   */
  public static resize<T>(array: T[], newSize: number, instanceToAdd: T): void {
    const oldSize = array.length;

    if (oldSize > newSize) {
      for (let size = oldSize; size > newSize; size--) {
        array.pop();
      }
    } else {
      for (let size = oldSize; size < newSize; size++) {
        const instanceToAddCopy = Object.assign({}, instanceToAdd);
        array.push(instanceToAddCopy);
      }
    }
  }
}

